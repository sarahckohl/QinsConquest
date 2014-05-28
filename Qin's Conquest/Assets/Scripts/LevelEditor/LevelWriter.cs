using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelWriter : MonoBehaviour {
	
	public List<GameObject> tiles = new List<GameObject>();
	
	public string fileName = "";
	private string basePath = "Assets/Resources/Levels/";
	
	public List<TextAsset> levels = new List<TextAsset>();
	public GetAllObjects allObjects;	// Assists us in rebuilding
	
	// This is for Drop Down Menu
	// GUIContent[] comboBoxList;
	List<GUIContent> comboBoxList = new List<GUIContent>();
	private ComboBox comboBoxControl;
	private GUIStyle listStyle = new GUIStyle();
	
	void Start() {
		for (int x = 0; x < tiles.Count; x++) {
			tiles[x].GetComponent<EditorHexBase>().setNeighbors(x, tiles.Count);
		}
		getAllLevels ();
		createDropDownList ();
		
	}
	
	void OnGUI() {
		comboBoxControl.Show ();
		
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 80, 90, 20), "Load Level")) {
			foreach (GameObject tile in tiles) {
				tile.GetComponent<EditorHexBase>().resetAll();
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag("EditorObject"))
					Destroy(obj);
				fileName = "";
			}
			if (comboBoxList[comboBoxControl.SelectedItemIndex].text != "Levels") {
				fileName = comboBoxList[comboBoxControl.SelectedItemIndex].text;
				LoadSelectedLevel ();
			}
		}
		
		fileName = GUI.TextField(new Rect(Screen.width - 200, Screen.height - 40, 90, 20), fileName, 25);
		
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 40, 90, 20), "Build Level")) {
			if (fileName == "") {
				Debug.Log ("Please Enter a name for this level");
			} else {
				writeFile ();
				getAllLevels ();
				createDropDownList ();
			}
		}
	}
	
	void LoadSelectedLevel() {
		using (StringReader reader = new StringReader(levels[comboBoxControl.SelectedItemIndex - 1].text)) {
			string line;
			string[] words;
			int elementNum = 0;
			while ((line = reader.ReadLine()) != null) {
				if (line == "-Start Tiles") {
					while ((line = reader.ReadLine()) != null && line != "-End Tiles") {
						words = line.Split(',');	// Splits comma
						if (words[0] == "Hex Blank")  {
							elementNum++;
							continue;
						}
						GameObject temp = Instantiate (allObjects.tileObject, tiles[elementNum].transform.position, Quaternion.identity) as GameObject;
						temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.4f);
						tiles[elementNum].GetComponent<EditorHexBase>().HexTile = temp;
						temp.GetComponent<SpriteRenderer>().sprite = allObjects.getTileSpriteByName(words[0]);
						temp.GetComponent<EditorTileObject>().tileName = words[0];
						elementNum++;
					}
				}
				
				if (line == "-Start Units") {
					while ((line = reader.ReadLine()) != null && line != "-End Units") {
						words = line.Split(',');	// Splits comma
						GameObject temp = Instantiate (allObjects.unitObject, tiles[int.Parse(words[1])].transform.position, Quaternion.identity) as GameObject;
						tiles[int.Parse(words[1])].GetComponent<EditorHexBase>().unitOnTile = temp;
						temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.6f);
						temp.GetComponent<SpriteRenderer>().sprite = allObjects.getUnitSpriteByName(words[0]);
						temp.GetComponent<EditorUnitObject>().unitName = words[0];
					}
				}
				
				if (line == "-Start Structures") {
					while ((line = reader.ReadLine()) != null && line != "-End Structures") {
						words = line.Split(',');	// Splits comma
						GameObject temp = Instantiate (allObjects.structureObject, tiles[int.Parse(words[1])].transform.position, Quaternion.identity) as GameObject;
						tiles[int.Parse(words[1])].GetComponent<EditorHexBase>().structureOnTile = temp;
						temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.6f);
						temp.GetComponent<SpriteRenderer>().sprite = allObjects.getStructureSpriteByName(words[0]);
						temp.GetComponent<EditorStructureObject>().structureName = words[0];
					}
				}
			}
		}
	}
	
	void createDropDownList() {
		comboBoxList.Clear ();
		
		comboBoxList.Add(new GUIContent("Levels"));
		foreach (TextAsset tex in levels) {
			comboBoxList.Add(new GUIContent(tex.name));
		}
		
		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
			listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
			listStyle.padding.right =
				listStyle.padding.top =
				listStyle.padding.bottom = 4;
		
		comboBoxControl = new ComboBox(new Rect(Screen.width - 200, Screen.height - 80, 90, 20),
				comboBoxList[0], comboBoxList.ToArray(), "button", "box", listStyle);
	}
	
	void getAllLevels() {
		levels.Clear();
		levels.AddRange(Resources.LoadAll<TextAsset>("Levels"));
	}
	
	public void writeFile() {
		List<string> tileData = new List<string>();
		List<string> unitData = new List<string>();
		List<string> structureData = new List<string>();
		
		string temp;
		
		//foreach(GameObject tile in tiles) {
		for (int x = 0; x < tiles.Count; x++) {
			// First part build tile string stucture
			temp = tiles[x].GetComponent<EditorHexBase>().HexTile.GetComponent<EditorTileObject>().tileName + ",";
			temp += tiles[x].transform.position.x + ",";
			temp += tiles[x].transform.position.y + ",";
			temp += tiles[x].transform.position.z + ",";
			temp += tiles[x].GetComponent<EditorHexBase>().neighbors[0] + ",";
			temp += tiles[x].GetComponent<EditorHexBase>().neighbors[1] + ",";
			temp += tiles[x].GetComponent<EditorHexBase>().neighbors[2] + ",";
			temp += tiles[x].GetComponent<EditorHexBase>().neighbors[3] + ",";
			temp += tiles[x].GetComponent<EditorHexBase>().neighbors[4] + ",";
			temp += tiles[x].GetComponent<EditorHexBase>().neighbors[5];
			
			tileData.Add(temp);
			
			if (tiles[x].GetComponent<EditorHexBase>().unitOnTile != null) {
				temp = tiles[x].GetComponent<EditorHexBase>().unitOnTile.GetComponent<EditorUnitObject>().unitName + ",";
				temp += x;
				
				unitData.Add (temp);
			}
			
			if (tiles[x].GetComponent<EditorHexBase>().structureOnTile != null) {
				temp = tiles[x].GetComponent<EditorHexBase>().structureOnTile.GetComponent<EditorStructureObject>().structureName + ",";
				temp += x;
				
				structureData.Add (temp);
			}
			
		}
	
		using (StreamWriter writer = new StreamWriter(basePath + fileName + ".txt")) {
			writer.WriteLine("-Start Tiles");
			foreach(string tile in tileData) writer.WriteLine(tile);
			writer.WriteLine("-End Tiles\n");
			
			writer.WriteLine("-Start Units");
			foreach(string unit in unitData) writer.WriteLine(unit);
			writer.WriteLine("-End Units\n");
			
			writer.WriteLine("-Start Structures");
			foreach(string structure in structureData) writer.WriteLine(structure);
			writer.WriteLine("-End Structures");
		}
		
		// Refrshes our asset so the editor has the new text document
		UnityEditor.AssetDatabase.Refresh();
	}
}
