using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSystem : MonoBehaviour {

	public Field gameField;
	public List<PlayerUnit> playerUnits = new List<PlayerUnit>();
	public List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
	public List<Village> bases = new List<Village> ();
	public int totalBases;
	public int turnCount;
	public bool turnEnd;
	public int totalPlayerUnits;

	// Use this for initialization
	void Start () {
		turnCount = 1; //First Turn
		turnEnd = false;
		totalBases = bases.Count;
	}
	
	// Update is called once per frame
	void Update () {
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
							if(enemy is EnemyArcher) {
								//move again if enemy unit is an Archer
								int moveAgain = enemy.onTile.GetComponent<HexTile>().moveEnemyArcherExtra(finalTarget);
								if(moveAgain >= 0) {
									//move again
									enemy.move (gameField.map[moveAgain]);
								} else {
									Debug.Log ("Don't move again.");
								}
							}
							enemy.attack (finalTarget.target);
						} else {
							enemy.onTile.GetComponent<HexTile>().cancelMovement (enemy.aggroRadius);
						}

					}
				}

				//empty the List of potentialTargets
				enemy.potentialTargets.Clear ();
			}
			checkLose ();
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
	
	public void checkWin() {
		if (totalBases <= 0) {
			GameState.stateDictionary[GameState.level] = true;
			Application.LoadLevel(4);
		}
	}
	
	public void checkLose() {
		if (totalPlayerUnits <= 0) {
			Application.LoadLevel(3);
		}
	}
}
