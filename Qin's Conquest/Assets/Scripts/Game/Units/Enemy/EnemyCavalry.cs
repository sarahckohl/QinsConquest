using UnityEngine;
using System.Collections;

public class EnemyCavalry : EnemyUnit{
	
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
		movement = 4;
		attackVal = 3 + onTile.GetComponent<HexTile> ().buff("atk");
		aggroRadius = movement + 1;
		attackRange = 1;
		defenseVal = 1 + onTile.GetComponent<HexTile> ().buff("def");
		unitCost = 3;
	}
	
	public override void attack(GameObject target) {
		base.attack(target);
	}
	
	public override void onDeath() {
		base.onDeath();
	}
}