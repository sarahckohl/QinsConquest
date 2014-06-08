using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSystem : MonoBehaviour {

	public Field gameField;
	public LevelReader reader;
	public List<PlayerUnit> playerUnits = new List<PlayerUnit>();
	public List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
	public List<Village> bases = new List<Village> ();
	public static int totalBases;
	public int turnCount;
	public bool turnEnd;
	public bool levelEnd;
	public static int remainingPlayers;

	// Use this for initialization
	void Start () {
		turnCount = 1; //First Turn
		turnEnd = false;
		levelEnd = false;
		if(GameState.level == "Han" || GameState.level == "Zhao"){
			totalBases = 2;
		}else if(GameState.level == "Qi" || GameState.level == "Wei" || GameState.level == "Yan"){
			totalBases = 3;
		}else if(GameState.level == "Chu"){
			totalBases = 4;
		}
		remainingPlayers = playerUnits.Count;
	}
	
	// Update is called once per frame
	void Update () {
		//This was a check for Hex ID implementation
		/*
		if (!temp) {
			foreach (GameObject h in gameField.map) {
				int tempI = h.GetComponent<HexTile> ().iD;
				Debug.Log (tempI);
			}
			temp = true;
		}
		*/

		if (totalBases <= 0) {
			//Game Level ends here
			levelEnd = true;
			//Debug.Log ("All bases have been destroyed");
			}

		if (remainingPlayers <= 0) {
			//Game Over here
			//Debug.Log ("Game Over");
			}

		if (turnEnd) {
			turnCount += 1;
			turnEnd = false;

			//Enemy Movement goes here
			foreach(EnemyUnit enemy in enemyUnits) {
				if(enemy is EnemyArcher) {
					Debug.Log (enemy.movement + " " + enemy.aggroRadius);
				}

				if(!enemy.isDead) {
				enemy.detectedPlayer = false;
				
				enemy.onTile.GetComponent <HexTile>().getAttackRangeEnemy(enemy, enemy.aggroRadius);
					if(enemy.detectedPlayer) {

						EnemyTargetModule finalTarget = null;
						int minHealth = 100; //For tracking the enemy with the lowest current health

						foreach(EnemyTargetModule ETM in enemy.potentialTargets) {

							if(ETM.target.GetComponent<PlayerUnit>().health < minHealth) {
								finalTarget = ETM;
								minHealth = ETM.target.GetComponent<PlayerUnit>().health;
							}
						}

						if(finalTarget.stopID >= 0) {
							enemy.move (gameField.map[finalTarget.stopID]);
							enemy.attack (finalTarget.target);
						} else {
							enemy.onTile.GetComponent<HexTile>().cancelMovement (enemy.aggroRadius);
						}

					}
				}

				//empty the List of potentialTargets
				enemy.potentialTargets.Clear ();
			}

			//Theoretically, the following should run only after the enemy units have already moved
			foreach (PlayerUnit player in playerUnits) {
				if(!player.isDead) {
					player.alreadyMoved = false;
					player.hasAttacked = false;
					player.renderer.material.color = player.originalColor;
				}
			}
	}
}
}
