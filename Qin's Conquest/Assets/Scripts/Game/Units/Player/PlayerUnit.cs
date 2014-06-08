using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit {

	public Color originalColor;
	private TurnSystem system;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		system = GameObject.FindGameObjectWithTag("System").GetComponent<TurnSystem>();
		originalColor = renderer.material.color;
		system.totalPlayerUnits++;
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
		hasAttacked = false;
		isDead = false;
		
		defenseVal = 2;
		unitCost = 1;
	}
	
	public override void select() {
		base.select ();
		if (!alreadyMoved) {
			onTile.GetComponent<HexTile> ().getMovementByRange (movement);
		}
		if (!hasAttacked) {
			onTile.GetComponent<HexTile> ().getAtkByRange (attackRange);
		}
	}
	
	public override void deSelect() {
		base.deSelect();
		if (!alreadyMoved) onTile.GetComponent<HexTile> ().cancelMovement (movement);
		if (!hasAttacked) onTile.GetComponent<HexTile> ().cancelAttack(attackRange);
	}
	
	public override void attack (GameObject obj) {
		base.attack (obj);
		if (alreadyMoved && hasAttacked)
			renderer.material.color = Color.gray;
	}
	
	public override void move(GameObject moveTo) {
		base.move (moveTo);
		onTile.GetComponent<HexTile>().cancelMovement(movement);
		if (!hasAttacked) onTile.GetComponent<HexTile> ().cancelAttack(attackRange);
		onTile.GetComponent<HexTile>().moveOff ();
		transform.position = moveTo.transform.position + new Vector3(0.0f, 0.0f, transform.position.z);
		onTile = moveTo;
		onTile.GetComponent<HexTile>().moveOn (gameObject);
		
		alreadyMoved = true;
		
		if (alreadyMoved && hasAttacked)
			renderer.material.color = Color.gray;
	}

	public override void onDeath() {
		isDead = true;
		Destroy(gameObject);
		onTile.GetComponent<HexTile>().moveOff();
		system.totalPlayerUnits--;
	}
}
