using UnityEngine;
using System.Collections;

public class Infantry : PlayerUnit{
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
	
	public override void setInitialUnitValues() {
		health = 2;
		movement = 2;
		attackRange = 1;
		attackVal = 2 + onTile.GetComponent<HexTile> ().buff("atk");
		defenseVal = 2 + onTile.GetComponent<HexTile> ().buff("def");
		unitCost = 1;
	}
	
	public override void attack(GameObject target) {
		base.attack(target);
	}
	
	public override void onDeath() {
		base.onDeath();
	}
}