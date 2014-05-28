using UnityEngine;
using System.Collections;


// Original controller for units
public class PrototypeUnitController : MonoBehaviour {
	/*
	public GetTiles tiles;

	private int movement = 3;
	
	public GameObject onTile;
	
	private bool selected = false;

	// Use this for initialization
	void Start () {
		onTile.GetComponent<HexTile>().moveOn (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				if (selected) {
					if (hit.collider.gameObject.GetComponent<HexTile>() != null) {
						if (hit.collider.gameObject.GetComponent<HexTile>().inRange) {
							if (!hit.collider.gameObject.GetComponent<HexTile>().taken) {
								moveUnit(hit.collider.gameObject);
							} else if (hit.collider.gameObject.GetComponent<HexTile>().takenBy.GetComponent<PrototypeStructure>() != null) {
								hit.collider.gameObject.GetComponent<HexTile>().takenBy.GetComponent<PrototypeStructure>().takeDamage(1);
							} else {
								deSelect();
							}
						} else {
							deSelect ();
						}
					} else {
						deSelect ();
					}
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
	
	void select() {
		selected = true;
		onTile.GetComponent<HexTile>().switchNeighborsOn(movement);
	}
	
	void deSelect() {
		selected = false;
		onTile.GetComponent<HexTile>().switchNeighborsOff(movement);
	}
	
	
	void moveUnit(GameObject moveTo) {
		onTile.GetComponent<HexTile>().switchNeighborsOff(movement);
		onTile.GetComponent<HexTile>().moveOff ();
		transform.position = moveTo.transform.position + new Vector3(0.0f, 0.0f, transform.position.z);
		onTile = moveTo;
		onTile.GetComponent<HexTile>().moveOn (gameObject);
		
		deSelect ();
		// onTile.GetComponent<HexTile>().switchNeighborsOn(movement);
	}*/
}
