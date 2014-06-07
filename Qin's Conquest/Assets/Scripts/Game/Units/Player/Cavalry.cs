using UnityEngine;
using System.Collections;

public class Cavalry : PlayerUnit {
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
	
	public override void setInitialUnitValues() {
		health = 3;
		movement = 4;
		attackRange = 1;
		attackVal = 3 + onTile.GetComponent<HexTile> ().buff("atk");
		defenseVal = 1 + onTile.GetComponent<HexTile> ().buff("def");
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