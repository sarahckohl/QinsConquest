using UnityEngine;
using System.Collections;

public class Archer : PlayerUnit {
	
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
		attackRange = 3;
		attackVal = 1;
		defenseVal = 3;
		unitCost = 1;
	}
	
	public override void attack(GameObject target) {
		base.attack(target);
		Debug.Log ("Shoot!");
	}
	
	public override void onDeath() {
		base.onDeath();
		Debug.Log ("This is impossible!!");
	}
}
