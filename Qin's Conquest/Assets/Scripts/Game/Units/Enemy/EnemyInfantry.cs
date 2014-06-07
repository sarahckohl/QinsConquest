using UnityEngine;
using System.Collections;

public class EnemyInfantry : EnemyUnit{
	
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
		attackVal = 2 + onTile.GetComponent<HexTile> ().buff("atk");
		aggroRadius = movement + 1;
		attackRange = 1;
		defenseVal = 2 + onTile.GetComponent<HexTile> ().buff("def");
		unitCost = 1;
	}
	
	public override void attack(GameObject target) {
		base.attack(target);
		Debug.Log ("Attack!");
	}
	
	public override void onDeath() {
		base.onDeath();
		Debug.Log ("This is impossible!!");
	}
}