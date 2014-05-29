using UnityEngine;
using System.Collections;

public class Wessman : PlayerUnit{

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
	
	public override void setInitialUnitValues() {
		health = 9001;
		movement = 2;
		attackVal = 9001;
	}
	
	public override void specialAttack(GameObject[] target) {
		base.specialAttack(target);
		Debug.Log ("BAM! Worksheets.");
	}
	
	public override void onDeath() {
		base.onDeath();
		Debug.Log ("This is impossible!!");
	}
}
