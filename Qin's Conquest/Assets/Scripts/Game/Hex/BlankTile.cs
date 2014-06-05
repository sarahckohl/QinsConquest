using UnityEngine;
using System.Collections;

public class BlankTile : HexTile {

	// An empty tile that players cannot select or move on
	// Used for level geography
	// Game art will be blank, level editor should show some tile

	void Start () {
		taken = true;
	}
	
	// Blank squares do nothing
	protected override void switchNeighborsOn(int step, int atkStep) {

	}
	
	protected override void switchNeighborsOff(int step) {
		
	}
}
