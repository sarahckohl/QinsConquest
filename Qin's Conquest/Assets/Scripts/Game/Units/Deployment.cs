using UnityEngine;
using System.Collections;

public class Deployment : MonoBehaviour {

	public TurnSystem system;
	public GameObject deploymentUnit;
	public GameObject unitObject;
	public GameObject onTile;

	public Sprite original;
	public Sprite highlighted;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void deployUnits() {
		if (deploymentUnit != null) {
			GameObject newUnit = (Instantiate(deploymentUnit, onTile.transform.position, Quaternion.identity) as GameObject);
			newUnit.transform.position = new Vector3(newUnit.transform.position.x, newUnit.transform.position.y, -0.5f);
			if (newUnit.GetComponent<Unit>() != null) {
				newUnit.GetComponent<Unit>().onTile = onTile;
			}
			system.playerUnits.Add (newUnit.GetComponent<PlayerUnit>());
			Destroy(unitObject);
		} else {
			onTile.GetComponent<HexTile>().moveOff();
		}
		Destroy(gameObject);
	}
	
	public void highlight() {
		if (deploymentUnit == null)
			gameObject.GetComponent<SpriteRenderer>().sprite = highlighted;
	}
	
	public void unHighlight() {
		if (deploymentUnit == null)
			gameObject.GetComponent<SpriteRenderer>().sprite = original;
	}
}
