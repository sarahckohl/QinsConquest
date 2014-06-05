using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitDeployment : MonoBehaviour {

	public List<GameObject> playerUnits = new List<GameObject>();
	public GameObject unitObject;
	public List<GameObject> deployers = new List<GameObject>();

	// Use this for initialization
	void Start () {
		playerUnits.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Units/Player"));
		for (int x = 0; x < playerUnits.Count; x++)
			if (playerUnits[x].name == "0_Placement Slot")
				playerUnits.RemoveAt(x);
		
		deployers.AddRange(GameObject.FindGameObjectsWithTag("UnitDeployment"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void deploy() {
		foreach(GameObject dep in deployers) {
			dep.GetComponent<Deployment>().deployUnits();
		}
		Destroy(gameObject);
	}
	
	void OnGUI() {
		playerUnitButtons();
	}
	
	void playerUnitButtons() {
		Event e = Event.current;
		for (int x = 0; x < playerUnits.Count; x++) {
			// if (GUI.Button (new Rect (100 + x*45, Screen.height - 95, 35, 35), (Texture2D)units[x].GetComponent<SpriteRenderer>().sprite.texture))
			
			// Button is replaced with these do allow dragging the object on mousedown instead of mouseup (Just feels better this way)
			Rect rect = new Rect (35 + x*60, Screen.height - 75, 50, 50);
			GUI.DrawTexture(rect, (Texture2D)playerUnits[x].GetComponent<SpriteRenderer>().sprite.texture, ScaleMode.ScaleToFit, true);
			if ((e.type == EventType.MouseDown) && rect.Contains (Event.current.mousePosition)){
				GameObject temp = Instantiate (unitObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity) as GameObject;
				temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.5f);
				temp.GetComponent<SpriteRenderer>().sprite = playerUnits[x].GetComponent<SpriteRenderer>().sprite;
				temp.GetComponent<UnitObject>().unit = playerUnits[x];
			}
		}
	}
}
