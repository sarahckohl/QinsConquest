using UnityEngine;
using System.Collections;

// This is used to get all the tiles in scene and set their neightbors
// Ideally, I will make a better method by the end of the week for this

public class GetTiles : MonoBehaviour {

	public GameObject[] tiles;
	
	// Distance to check if tile is in range
	private double dist = Mathf.Sqrt(1.0f + Mathf.Pow(0.8f, 2.0f));

	// Use this for initialization
	void Awake () {
		tiles = GameObject.FindGameObjectsWithTag("Tile");
		
		for(int xy = 0; xy < tiles.Length; xy++) {
			float x, y;
			float bx, by;
			
			foreach (GameObject t in tiles) {
				if (tiles[xy].GetComponent<HexTile>().neighbors.Count == 6) break;
				
				x = t.transform.position.x;
				y = t.transform.position.y;
				
				bx = tiles[xy].transform.position.x;
				by = tiles[xy].transform.position.y;
				
				if (x == bx && y == by) continue;
				
				if (Mathf.Sqrt(Mathf.Pow(bx - x, 2.0f) + Mathf.Pow (by - y, 2.0f)) < dist) {
					//tiles[xy].GetComponent<HexTile>().neighbors.Add (t);
				}
			}
		}
	}
}
