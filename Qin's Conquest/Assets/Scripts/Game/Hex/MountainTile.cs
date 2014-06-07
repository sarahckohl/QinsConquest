using UnityEngine;
using System.Collections;

public class MountainTile : HexTile {

	void Start() {
		moveDecrement = 2;
		atkBuff = 1;
		defBuff = 1;
	}
}
