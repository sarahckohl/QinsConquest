using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSystem : MonoBehaviour {

	public Field gameField;
	public List<PlayerUnit> playerUnits = new List<PlayerUnit>();
	public List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
	public List<Village> bases = new List<Village> ();
	public static int totalBases;
	public int turnCount;
	public bool turnEnd;
	public bool temp = false;

	// Use this for initialization
	void Start () {
		turnCount = 1; //First Turn
		turnEnd = false;
		totalBases = 0;
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

		if (totalBases >= bases.Count) {
			//Game Level ends here
			//Debug.Log ("All bases have been destroyed");
			}

		if (turnEnd) {
			turnCount += 1;
			turnEnd = false;
			//EnemyTargetModule is a workaround for getting a Player that is within an enemy unit's range
			//Since Hex Tiles are not saved between calls. There is no gameObject for this; just a class found is Misc folder
			/*
			EnemyTargetModule.foundTarget = false;
			EnemyTargetModule.targetID = -1;
			EnemyTargetModule.stopID = -1;
			*/
		//	Debug.Log ("Turn : " + turnCount);
			/*
			//Testing something out
			//Debug.Log (enemyUnits.Count);
			EnemyUnit temp = enemyUnits[0];
			//Debug.Log ("Enemy ID: " + temp.onTile.GetComponent <HexTile>().getAttackRangeEnemy(temp.attackRange));
			temp.onTile.GetComponent <HexTile>().getAttackRangeEnemy(temp.attackRange);
			if(EnemyTargetModule.stopID >= 0) {
				temp.move (gameField.map[EnemyTargetModule.stopID]);
			} else {
				temp.onTile.GetComponent<HexTile>().cancelMovement (temp.attackRange);
			}


			Debug.Log ("Boolean result: " + EnemyTargetModule.foundTarget);
			Debug.Log ("Target Int result: " + EnemyTargetModule.targetID);
			Debug.Log ("enemy stop Int result: " + EnemyTargetModule.stopID);
			*/

			//temp.onTile.GetComponent <HexTile>().getAttackRangeEnemy(temp.attackRange);
		

			//Enemy Movement goes here
			foreach(EnemyUnit enemy in enemyUnits) {
				if(enemy is EnemyArcher) {
					Debug.Log ("We have an Archer here!");
				}

				if(!enemy.isDead) {
				EnemyTargetModule.foundTarget = false;
				EnemyTargetModule.targetID = -1;
				EnemyTargetModule.stopID = -1;
				enemy.onTile.GetComponent <HexTile>().getAttackRangeEnemy(enemy.aggroRadius);
				if(EnemyTargetModule.stopID >= 0) {
					enemy.move (gameField.map[EnemyTargetModule.stopID]);
				} else {
					enemy.onTile.GetComponent<HexTile>().cancelMovement (enemy.aggroRadius);
				}
				}
			}

			//Theoretically, the following should run only after the enemy units have already moved
			foreach (PlayerUnit player in playerUnits) {
				player.alreadyMoved = false;
				player.hasAttacked = false;
				player.renderer.material.color = player.originalColor;
			}
	}
}
}
