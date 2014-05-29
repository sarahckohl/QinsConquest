using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexTile : MonoBehaviour {

	public bool taken = false;
	public Field field;
	
	// public List<GameObject> neighbors = new List<GameObject>();
	public List<int> neighbors = new List<int>();
	
	public bool inRange = false;
	public GameObject takenBy;
	
	public int moveDecrement = 1;
	
	protected Color originalColor;
	
	void Awake() {
		originalColor = GetComponent<SpriteRenderer>().color;
	}
	
	public void moveOn(GameObject byObject) {
		taken = true;
		takenBy = byObject;
	}
	
	public void moveOff() {
		taken = false;
		takenBy = null;
	}
	
	public void getMovementByRange(int step) {
		if (step > 0) {
			HexTile temp;
			
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				temp = field.map[nei].GetComponent<HexTile>();
				if (!temp.taken) {
					temp.switchNeighborsOn(step);
				} else if (temp.name != "Hex Blank" && temp.takenBy.tag != "Player") {
					temp.enemyOnTile();
				}
			}
		}
	}
	
	public void cancelMovement(int step) {
		if (step > 0) {
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				field.map[nei].GetComponent<HexTile>().switchNeighborsOff(step);
			}
		}
	}
	
	protected virtual void enemyOnTile() {
		inRange = true;
		GetComponent<SpriteRenderer>().color = Color.red;
	}
	
	protected virtual void switchNeighborsOn(int step) {
		inRange = true;
		step -= moveDecrement;
		
		GetComponent<SpriteRenderer>().color = Color.yellow;
		
		if (step > 0) {
			HexTile temp;
			
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				temp = field.map[nei].GetComponent<HexTile>();
				if (!temp.taken) {
					temp.switchNeighborsOn(step);
				} else if (temp.takenBy.tag != "Player") {
					temp.enemyOnTile();
				}
			}
		}
	}
	
	protected virtual void switchNeighborsOff(int step) {
		inRange = false;
		step -= moveDecrement;
		
		GetComponent<SpriteRenderer>().color = originalColor;
		if (step > 0) {
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				field.map[nei].GetComponent<HexTile>().switchNeighborsOff(step);
			}
		}
	}
}
