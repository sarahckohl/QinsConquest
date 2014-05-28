﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetAllObjects : MonoBehaviour {
	
	public List<GameObject> tiles = new List<GameObject>();
	public List<GameObject> units = new List<GameObject>();
	public List<GameObject> structures = new List<GameObject>();
	
	public GameObject tileObject;
	public GameObject unitObject;
	public GameObject structureObject;
	
	private int state = 0;

	// Use this for initialization
	void Start () {
		tiles.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Tiles"));
		for (int x = 0; x < tiles.Count; x++)
			if (tiles[x].GetComponent<HexTile>().name == "Hex Blank")
				tiles.RemoveAt(x);	// So the blank tile won't shown since it's unneccesary
		units.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Units"));
		structures.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Structures"));
	}
	
	// This will be used to imitate a tab style menu
	void OnGUI() {
	
		// Which menu to have open
		if (state == 0) tileButtons ();
		else if (state == 1) unitButtons ();
		else if (state == 2) structureButtons ();
	
		// Changes state to switch menu shown
		if (GUI.Button (new Rect (10, Screen.height - 95, 70, 25), "Tiles")) {
			state = 0;
		}
		
		if (GUI.Button (new Rect (10, Screen.height - 65, 70, 25), "Units")) {
			state = 1;
		}
		
		if (GUI.Button (new Rect (10, Screen.height - 35, 70, 25), "Structures")) {
			state = 2;
		}
	}
	
	public Sprite getTileSpriteByName(string objName) {
		foreach(GameObject objs in tiles)
			if (objs.name == objName)
				return objs.GetComponent<SpriteRenderer>().sprite;
		return null;
	}
	
	public Sprite getUnitSpriteByName(string objName) {
		foreach(GameObject objs in units)
			if (objs.name == objName)
				return objs.GetComponent<SpriteRenderer>().sprite;
		return null;
	}
	
	public Sprite getStructureSpriteByName(string objName) {
		foreach(GameObject objs in structures)
			if (objs.name == objName)
				return objs.GetComponent<SpriteRenderer>().sprite;
		return null;
	}
	
	void tileButtons() {
		Event e = Event.current;
		for (int x = 0; x < tiles.Count; x++) {
			
			//if (GUI.Button (new Rect (100 + x*45, Screen.height - 95, 35, 35), (Texture2D)tiles[x].GetComponent<SpriteRenderer>().sprite.texture))
			
			// Button is replaced with these do allow dragging the object on mousedown instead of mouseup (Just feels better this way)
			Rect rect = new Rect (100 + x*45, Screen.height - 95, 35, 35);
			GUI.DrawTexture(rect, (Texture2D)tiles[x].GetComponent<SpriteRenderer>().sprite.texture, ScaleMode.ScaleToFit, true);
			if ((e.type == EventType.MouseDown) && rect.Contains (Event.current.mousePosition)){
				GameObject temp = Instantiate (tileObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity) as GameObject;
				temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.5f);
				temp.GetComponent<SpriteRenderer>().sprite = tiles[x].GetComponent<SpriteRenderer>().sprite;
				temp.GetComponent<EditorTileObject>().tileName = tiles[x].name;
			}
		}
	}
	
	void unitButtons() {
		Event e = Event.current;
		for (int x = 0; x < units.Count; x++) {
			// if (GUI.Button (new Rect (100 + x*45, Screen.height - 95, 35, 35), (Texture2D)units[x].GetComponent<SpriteRenderer>().sprite.texture))
			
			// Button is replaced with these do allow dragging the object on mousedown instead of mouseup (Just feels better this way)
			Rect rect = new Rect (100 + x*45, Screen.height - 95, 35, 35);
			GUI.DrawTexture(rect, (Texture2D)units[x].GetComponent<SpriteRenderer>().sprite.texture, ScaleMode.ScaleToFit, true);
			if ((e.type == EventType.MouseDown) && rect.Contains (Event.current.mousePosition)){
				GameObject temp = Instantiate (unitObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity) as GameObject;
				temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.5f);
				temp.GetComponent<SpriteRenderer>().sprite = units[x].GetComponent<SpriteRenderer>().sprite;
				temp.GetComponent<EditorUnitObject>().unitName = units[x].name;
			}
		}
	}
	
	void structureButtons() {
		Event e = Event.current;
		for (int x = 0; x < structures.Count; x++) {
			// if (GUI.Button (new Rect (100 + x*45, Screen.height - 95, 35, 35), (Texture2D)structures[x].GetComponent<SpriteRenderer>().sprite.texture))
			
			// Button is replaced with these do allow dragging the object on mousedown instead of mouseup (Just feels better this way)
			Rect rect = new Rect (100 + x*45, Screen.height - 95, 35, 35);
			GUI.DrawTexture(rect, (Texture2D)structures[x].GetComponent<SpriteRenderer>().sprite.texture, ScaleMode.ScaleToFit, true);
			if ((e.type == EventType.MouseDown) && rect.Contains (Event.current.mousePosition)){
				GameObject temp = Instantiate (structureObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity) as GameObject;
				temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.5f);
				temp.GetComponent<SpriteRenderer>().sprite = structures[x].GetComponent<SpriteRenderer>().sprite;
				temp.GetComponent<EditorStructureObject>().structureName = structures[x].name;
			}
		}
	}
}