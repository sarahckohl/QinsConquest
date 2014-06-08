using UnityEngine;
using System.Collections;

public class Structure : MonoBehaviour, ISelectable, IDamageable<int> {

	public GameObject onTile;

	public int health;
	public bool selected;
	public bool isDestroyed;
	private TurnSystem system;

	// Use this for initialization
	protected virtual void Start () {
		system = GameObject.FindGameObjectWithTag("System").GetComponent<TurnSystem>();
		setInitialStructureValues();
		onTile.GetComponent<HexTile>().moveOn (gameObject);
		isDestroyed = false;
	}
	
	public virtual void setInitialStructureValues() {
		health = 2;
	}
	
	// For a building, these could be used for displaying stats/health etc
	public virtual void select() {
		selected = true;
	}
	
	public virtual void deSelect() {
		selected = false;
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
		isDestroyed = true;
		Destroy(gameObject);
		system.totalBases--;
		system.checkWin();
		onTile.GetComponent<HexTile>().moveOff();
	}
}
