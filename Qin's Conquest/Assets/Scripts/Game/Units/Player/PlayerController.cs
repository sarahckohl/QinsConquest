﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject selectedUnit;
	public SelectionStats stats;
	
	// Use this for initialization
	void Start () {
		selectedUnit = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				if (selectedUnit != null) {
					if (selectedUnit.GetComponent<PlayerUnit>() != null && (!selectedUnit.GetComponent<PlayerUnit>().alreadyMoved ||
					    	!selectedUnit.GetComponent<PlayerUnit>().hasAttacked)) {
						playerUnitCommands (hit.collider.gameObject);
					}
					deSelectUnit ();
				} else {
					if (hit.collider.gameObject.GetComponent<Unit>() != null ||
							hit.collider.gameObject.GetComponent<Structure>() != null) {
						selectUnit (hit.collider.gameObject);
					}
				}
			} else {
				deSelectUnit ();
			}
		}
	}
	
	void playerUnitCommands(GameObject clickedObject) {
		if (clickedObject.GetComponent<HexTile>() != null) {
			if (clickedObject.GetComponent<HexTile>().inRange) {
				if (!clickedObject.GetComponent<HexTile>().taken) {
					selectedUnit.GetComponent<PlayerUnit>().move(clickedObject);
				}
			}
		} else if (clickedObject.GetComponent<EnemyUnit>() != null) {
			if (clickedObject.GetComponent<EnemyUnit>().onTile.GetComponent<HexTile>().inAtkRange) {
				selectedUnit.GetComponent<PlayerUnit>().attack (clickedObject);
			}
		} else if (clickedObject.GetComponent<Structure>() != null) {
			if (clickedObject.GetComponent<Structure>().onTile.GetComponent<HexTile>().inAtkRange) {
				selectedUnit.GetComponent<PlayerUnit>().attack (clickedObject);
			}
		}
	}
	
	void selectUnit(GameObject select) {
		selectedUnit = select;
		(selectedUnit.GetComponent(typeof(ISelectable)) as ISelectable).select ();
		stats.setSelection(select);
	}
	
	void deSelectUnit() {
		if (selectedUnit != null) {
			(selectedUnit.GetComponent(typeof(ISelectable)) as ISelectable).deSelect ();
			selectedUnit = null;
			stats.deSelection();
		}
	}
}
