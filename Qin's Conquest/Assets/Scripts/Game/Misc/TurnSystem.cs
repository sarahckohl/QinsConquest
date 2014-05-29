using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSystem : MonoBehaviour {

	public Field gameField;
	public List<GameObject> playerUnits = new List<GameObject>();
	public int turnCount;
	public bool turnEnd;

	// Use this for initialization
	void Start () {
		turnCount = 1; //First Turn
		turnEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
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
		if (count >= activePlayers || turnEnd) {
			turnCount += 1;
			turnEnd = false;
			Debug.Log ("Turn : " + turnCount);

			//Theoretically, the following should run only after the enemy units have already moved
			foreach (GameObject player in playerUnits) {
				player.GetComponent<Unit>().alreadyMoved = false;
			}
	}
}
}
