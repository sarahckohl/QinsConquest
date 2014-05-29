using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, ISelectable, IDamageable<int>, IMoveable<GameObject>, IAttackable<GameObject, GameObject[]> {

	public GameObject onTile;

	public int health;
	public bool selected;
	public bool alreadyMoved;
	public bool isDead;
	
	public int movement;
	public int attackVal;

	// Use this for initialization
	protected virtual void Start () {
		setInitialUnitValues ();
		onTile.GetComponent<HexTile>().moveOn(gameObject);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
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
								attack (hit.collider.gameObject.GetComponent<HexTile>().takenBy);
							}
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
	
	public virtual void setInitialUnitValues() {
		health = 10;
		movement = 1;
		attackVal = 2;
		alreadyMoved = false;
		isDead = false;
	}
	
	public virtual void select() {
		if (!alreadyMoved) {
			selected = true;
			onTile.GetComponent<HexTile> ().getMovementByRange (movement);
		}
	}
	
	public virtual void deSelect() {
		//if (!alreadyMoved) {
			selected = false;
			onTile.GetComponent<HexTile> ().cancelMovement (movement);
		//}
	}
	
	public virtual void move(GameObject moveTo) {
		onTile.GetComponent<HexTile>().cancelMovement(movement);
		onTile.GetComponent<HexTile>().moveOff ();
		transform.position = moveTo.transform.position + new Vector3(0.0f, 0.0f, transform.position.z);
		onTile = moveTo;
		onTile.GetComponent<HexTile>().moveOn (gameObject);
		alreadyMoved = true;
	}
	
	public virtual void attack(GameObject obj) {
		((IDamageable<int>)obj.GetComponent(typeof(IDamageable<int>))).takeDamage(attackVal);
	}
	
	public virtual void specialAttack(GameObject[] obj) {
	}
	
	public virtual void takeDamage(int damage) {
		health -= Mathf.Abs(damage);
		if (health <= 0) {
			onDeath ();
		}
	}
	
	public virtual void healDamage(int heal) {
		health += Mathf.Abs(heal);
	}
	
	public virtual void onDeath() {
		isDead = true;
		Destroy(gameObject);
		onTile.GetComponent<HexTile>().moveOff();
	}
}
