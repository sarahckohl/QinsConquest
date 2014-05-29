using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit {
	
	protected override void Start () {
		base.Start ();
	}
	
	protected override void Update () {
	}
	
	public override void setInitialUnitValues() {
		health = 1;
	    movement = 1;
		attackVal = 2;

		isDead = false;
		
		defenseVal = 2;
		unitCost = 1;
	
		
	}
	
	public override void select() {
		base.select ();
	}
	
	public override void deSelect() {
		base.deSelect ();
	}
	
	public override void move(GameObject moveTo) {
		// AI commands go gere
	}
	
	public override void attack(GameObject obj) {
		base.attack (obj);
	}

	
	public override void onDeath() {
		base.onDeath();
	}
}


