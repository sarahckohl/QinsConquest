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
	public int defenseVal;
	public int unitCost;

	// Use this for initialization
	protected virtual void Start () {
		setInitialUnitValues ();
		onTile.GetComponent<HexTile>().moveOn(gameObject);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
	
	public virtual void setInitialUnitValues() {
		health = 1;
		movement = 1;
		attackVal = 2;

		alreadyMoved = false;
		isDead = false;

		defenseVal = 2;
		unitCost = 1;
	}
	
	public virtual void select() {
		// Display units stats in HUD
		selected = true;
	}
	
	public virtual void deSelect() {
		selected = false;
	}
	
	public virtual void move(GameObject moveTo) {
		// Stuff
		/*
		onTile.GetComponent<HexTile>().cancelMovement(movement);
		onTile.GetComponent<HexTile>().moveOff ();
		transform.position = moveTo.transform.position + new Vector3(0.0f, 0.0f, transform.position.z);
		onTile = moveTo;
		onTile.GetComponent<HexTile>().moveOn (gameObject);

		alreadyMoved = true; */
	}
	
	public virtual void attack(GameObject obj) {
				((IDamageable<int>)obj.GetComponent (typeof(IDamageable<int>))).takeDamage (attackVal);
				alreadyMoved = true;
		}

	public virtual void specialAttack(GameObject[] obj) {
	}
	
	public virtual void takeDamage(int damage) {
		int dmg = damage;
		while(dmg > 0 || health > 0){
			if(defenseVal > 0){
				--defenseVal;
			}else{
				--health;
			}
			--dmg;
		}
		if (health <= 0) {
			onDeath ();
		}
		Debug.Log ("Damage");
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
