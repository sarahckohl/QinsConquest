using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitObject : MonoBehaviour {
	
	public GameObject unit;
	public Vector3 targetTilePos;
	
	private Vector3 initMPos;
	
	private List<GameObject> collidingTiles = new List<GameObject>();
	private int targetTile;
	private float tempDist, dist;
	
	private bool set = false;
	private bool justSpawn = true;
	
	// Use this for initialization
	void Start () {
		dist = 5000;
	}
	
	// Update is called once per frame
	void Update () {
		if (!set) {
			// Goes through collision list and highlights the closest hex
			for (int x = 0; x < collidingTiles.Count; x++) {
				tempDist = Mathf.Sqrt(Mathf.Pow(collidingTiles[x].transform.position.x - transform.position.x, 2) + Mathf.Pow(collidingTiles[x].transform.position.y - transform.position.y, 2));
				if (collidingTiles.Count > targetTile) {
					dist = Mathf.Sqrt(Mathf.Pow(collidingTiles[targetTile].transform.position.x - transform.position.x, 2) + Mathf.Pow(collidingTiles[targetTile].transform.position.y - transform.position.y, 2));
					if (tempDist <= dist) {
						dist = tempDist;
						collidingTiles[targetTile].GetComponent<Deployment>().unHighlight();
						targetTile = x;
						collidingTiles[targetTile].GetComponent<Deployment>().highlight();
					} 
				} else {
					dist = tempDist;
					targetTile = x;
					collidingTiles[targetTile].GetComponent<Deployment>().highlight();
				}
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "UnitDeployment") {
			if(!collidingTiles.Contains(other.gameObject) && other.GetComponent<Deployment>().deploymentUnit == null) {
				collidingTiles.Add (other.gameObject);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<Deployment>() != null) {
			collidingTiles.Remove(other.gameObject);
			other.gameObject.GetComponent<Deployment>().unHighlight();
		}
	}
	
	// This one is for when level a loaded into editor
	void OnTriggerStay2D(Collider2D other) {
		if (justSpawn) {
			if (other.tag == "UnitDeployment") {
				justSpawn = false;
				set = true;
				collidingTiles.Clear();
				targetTile = 0;
				collidingTiles.Add (other.gameObject);
			}
		}
	}
	
	void OnMouseDown() {
		if (set) {
			collidingTiles[targetTile].GetComponent<Deployment>().deploymentUnit = null;
			collidingTiles[targetTile].GetComponent<Deployment>().unitObject = null;
			set = false;
		}
	}
	
	void OnMouseUp() {
		if (collidingTiles.Count > targetTile) {
			//collidingTiles[targetTile].GetComponent<EditorHexBase>().unHighlight();
			foreach (GameObject colli in collidingTiles) {
				colli.GetComponent<Deployment>().unHighlight();
			}
			targetTilePos = collidingTiles[targetTile].transform.position;
			targetTilePos.z = -0.5f;
			transform.position = targetTilePos;
			collidingTiles[targetTile].GetComponent<Deployment>().deploymentUnit = unit;
			collidingTiles[targetTile].GetComponent<Deployment>().unitObject = gameObject;
			set = true;
			targetTile = 0;
		} else {
			Destroy(gameObject);
		}
	}
	
	void OnMouseDrag() {
		justSpawn = false;
		initMPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		initMPos.z = -0.7f;
		transform.position = initMPos;
	}
}
