using UnityEngine;
using System.Collections;

public class FieldGUI : MonoBehaviour {
	public TurnSystem system;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 100, 100, 50), "Turn End")) {
			system.turnEnd = true;
		}
	}
}
