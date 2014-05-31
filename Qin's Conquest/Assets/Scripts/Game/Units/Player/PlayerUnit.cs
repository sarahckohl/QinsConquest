using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				if (selected) {
					if (hit.collider.gameObject.GetComponent<HexTile>() != null) {
						if (hit.collider.gameObject.GetComponent<HexTile>().inRange) {
							if (!hit.collider.gameObject.GetComponent<HexTile>().taken) {
								move(hit.collider.gameObject);
							} else if ((IDamageable<int>)hit.collider.gameObject.GetComponent<HexTile>().takenBy.GetComponent(typeof(IDamageable<int>)) != null &&
							           hit.collider.gameObject.GetComponent<HexTile>().takenBy.tag != "Player") {
							    // Currently this is used for structures cause they have no collision box, should
							    // be changed later
								attack (hit.collider.gameObject.GetComponent<HexTile>().takenBy);
							}
						}
					} else if (hit.collider.gameObject.GetComponent<EnemyUnit>() != null) {
						// Change inrange with in attackrange instead later on
						if (hit.collider.gameObject.GetComponent<EnemyUnit>().onTile.GetComponent<HexTile>().inRange) {
							attack (hit.collider.gameObject);
						}
					}
					deSelect ();
				} else {
					if (hit.collider.gameObject == gameObject) {
						select ();
					}
				}
			} else {
				deSelect ();
			}
		}
	}
	
	public override void setInitialUnitValues() {
		health = 1;
		movement = 1;
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
	}
}
