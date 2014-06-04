﻿using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit {

	public Color originalColor;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		originalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	protected override void Update () {
		// Moved controls to PlayerController, I have a copy of the old code if needed - Francis
	}
	
	public override void setInitialUnitValues() {
		health = 1;
		movement = 1;
		attackRange = 1;
		attackVal = 2;
		
		alreadyMoved = false;
		isDead = false;
		
		defenseVal = 2;
		unitCost = 1;
	}
	
	public override void select() {
		base.select ();
		if (!alreadyMoved) {
			onTile.GetComponent<HexTile> ().getMovementByRange (movement);
		}
	}
	
	public override void deSelect() {
		base.deSelect();
		onTile.GetComponent<HexTile> ().cancelMovement (movement);
	}
	
	public override void move(GameObject moveTo) {
		base.move (moveTo);
		onTile.GetComponent<HexTile>().cancelMovement(movement);
		onTile.GetComponent<HexTile>().moveOff ();
		transform.position = moveTo.transform.position + new Vector3(0.0f, 0.0f, transform.position.z);
		onTile = moveTo;
		onTile.GetComponent<HexTile>().moveOn (gameObject);
		
		alreadyMoved = true;
		renderer.material.color = Color.gray;
	}
}
