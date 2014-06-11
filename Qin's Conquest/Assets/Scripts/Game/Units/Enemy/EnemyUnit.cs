using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyUnit : Unit {
	public int aggroRadius;
	public bool detectedPlayer = false;
	public List<EnemyTargetModule> potentialTargets = new List<EnemyTargetModule>();

	protected override void Start () {
		base.Start ();
	}
	
	protected override void Update () {
	}
	
	public override void setInitialUnitValues() {
		health = 1;
	    movement = 1;
		attackVal = 2;
		aggroRadius = movement + 1;
		attackRange = 1;
		isDead = false;
		
		defenseVal = 2;
		unitCost = 1;
	
		
	}
	
	public override void select() {
		base.select ();
		onTile.GetComponent<HexTile> ().getMovementByRange (movement);
		onTile.GetComponent<HexTile> ().getAtkByRange (attackRange);
	}
	
	public override void deSelect() {
		base.deSelect ();
		onTile.GetComponent<HexTile> ().cancelMovement (movement);
		onTile.GetComponent<HexTile> ().cancelAttack(attackRange);
	}

	//moveTo is a HexTile 
	//Idea of Enemy AI Movement is to move whenever the player unit gets within its attack, not movement, range
	public override void move(GameObject moveTo) {
		// AI commands go here
		base.move (moveTo);
		onTile.GetComponent<HexTile>().cancelMovement(attackRange);
		onTile.GetComponent<HexTile>().moveOff ();
		transform.position = moveTo.transform.position + new Vector3(0.0f, 0.0f, transform.position.z);
		onTile = moveTo;
		onTile.GetComponent<HexTile>().moveOn (gameObject);
	}
	
	public override void attack(GameObject obj) {
		base.attack (obj);
	}

	
	public override void onDeath() {
		base.onDeath();
	}
	
}


