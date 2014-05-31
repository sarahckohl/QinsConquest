using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSystem : MonoBehaviour {

	public Field gameField;
	public List<PlayerUnit> playerUnits = new List<PlayerUnit>();
	public List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
	public int turnCount;
	public bool turnEnd;
	public bool temp = false;

	// Use this for initialization
	void Start () {
		turnCount = 1; //First Turn
		turnEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (!temp) {
			foreach (GameObject h in gameField.map) {
				int tempI = h.GetComponent<HexTile> ().iD;
				Debug.Log (tempI);
			}
			temp = true;
		}
	
		int count = 0;
		int activePlayers = 0;
		foreach (GameObject player in playerUnits) {
			if(!player.GetComponent<Unit>().isDead) {
				activePlayers++;
				if(player.GetComponent<Unit>().alreadyMoved) {
					count++;
				}
			}
			//Checks if all playable units have moved yet
		}
		//Debug.Log ("active :" + activePlayers +" count : " + count);
		*/
		if (turnEnd) {
			turnCount += 1;
			turnEnd = false;
			Debug.Log ("Turn : " + turnCount);

			//Testing something out
			Debug.Log (enemyUnits.Count);
			//EnemyUnit temp = enemyUnits[1];

			//temp.onTile.GetComponent <HexTile>().getAttackRangeEnemy(temp.attackRange);
		

			//Enemy Movement goes here
			foreach(EnemyUnit enemy in enemyUnits) {
				enemy.onTile.GetComponent <HexTile>().getAttackRangeEnemy(enemy.attackRange);
				enemy.onTile.GetComponent<HexTile>().cancelMovement(enemy.attackRange);
			}

			//Theoretically, the following should run only after the enemy units have already moved
			foreach (PlayerUnit player in playerUnits) {
				player.alreadyMoved = false;
			}
	}
}
}
