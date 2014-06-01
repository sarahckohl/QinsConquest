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
		attackVal = 3;
		attackRange = movement + 1;
		defenseVal = 1;
		unitCost = 3;
	}
	
	public override void attack(GameObject target) {
		base.attack(target);
		Debug.Log ("Charge!");
	}
	
	public override void onDeath() {
		base.onDeath();
		Debug.Log ("This is impossible!!");
	}
}