using UnityEngine;
using System.Collections;

public class Village : Structure {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	public override void setInitialStructureValues() {
		health = 2;
	}
}
