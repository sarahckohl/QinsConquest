﻿using UnityEngine;
using System.Collections;

public class EnemyArcher : EnemyUnit{
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
	
	public override void setInitialUnitValues() {
		health = 1;
		movement = 2;
		attackVal = 1 + onTile.GetComponent<HexTile> ().buff("atk");
		attackRange = 1;
		aggroRadius = movement + 2;
		defenseVal = 3 + onTile.GetComponent<HexTile> ().buff("def");
		unitCost = 1;
	}
	
	public override void attack(GameObject target) {
		base.attack(target);
	}
	
	public override void onDeath() {
		base.onDeath();
	}
}
