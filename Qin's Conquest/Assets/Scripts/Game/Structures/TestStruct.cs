using UnityEngine;
using System.Collections;

public class TestStruct : Structure {

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	public override void setInitialStructureValues() {
		health = 1;
	}
	
	public override void onDeath() {
		base.onDeath();
	}
}
