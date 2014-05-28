using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorHexBase : MonoBehaviour {

	public GameObject HexTile;
	private GameObject blankTile;

	public GameObject unitOnTile;
	public GameObject structureOnTile;
	
	private int totalTiles;
	private int elementInList;	// This objects index in the list
	
	// Neightbors of this hex, clockwise starting from top right position
	// Number is their position in the array of hexes
	// Hexes on the edge will have -1 as their value
	public List<int> neighbors = new List<int>();
	
	public Sprite original;
	public Sprite highlighted;

	// Use this for initialization
	void Start () {
		blankTile = HexTile;
	}
	
	// Sets neightbors based on index on list;
	public void setNeighbors(int index, int total) {
		elementInList = index;
		totalTiles = total;
		
		if ((index + 1)% 29 == 0) {
			neighbors.Add(-1);
			neighbors.Add(-1);
			neighbors.Add(-1);
		} else {
			neighbors.Add(elementChecker (-14));
			if ((index - 13)%29 == 0) neighbors.Add(-1);
			else neighbors.Add(elementChecker (1));
			neighbors.Add(elementChecker (15));
		}
		
		if ((index - 14)%29 == 0) {
			neighbors.Add(-1);
			neighbors.Add(-1);
			neighbors.Add(-1);
		} else {
			neighbors.Add(elementChecker (14));
			if (index%29 == 0) neighbors.Add(-1);
			else neighbors.Add(elementChecker (-1));
			neighbors.Add(elementChecker (-15));
		}
		
		/*
		neighbors.Add(elementChecker (-14));
		neighbors.Add(elementChecker (1));
		neighbors.Add(elementChecker (15));
		neighbors.Add(elementChecker (14));
		neighbors.Add(elementChecker (-1));
		neighbors.Add(elementChecker (-15));
		*/
	}
	
	int elementChecker(int change) {
		if ((elementInList + change) < 0 || (elementInList + change) > totalTiles - 1) {
			return -1;
		}
		return elementInList + change;
	}
	
	public void highlight() {
		gameObject.GetComponent<SpriteRenderer>().sprite = highlighted;
	}
	
	public void unHighlight() {
		gameObject.GetComponent<SpriteRenderer>().sprite = original;
	}
	
	public void resetTile() {
		HexTile = blankTile;
	}
	
	public void resetAll() {
		HexTile = blankTile;
		unitOnTile = null;
		structureOnTile = null;
	}
}
