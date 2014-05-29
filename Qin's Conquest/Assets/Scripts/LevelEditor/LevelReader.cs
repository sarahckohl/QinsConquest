using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelReader : MonoBehaviour {

	public string fileName = "";
	
	public List<GameObject> tileType;	// Holds all possible tile types
	public Field field;
	public TurnSystem system;
	
	public List<GameObject> tiles = new List<GameObject>();
	public List<GameObject> units = new List<GameObject>();
	public List<GameObject> structures = new List<GameObject>();

	private Dictionary<string, GameObject> tileDictionary = new Dictionary<string, GameObject>();
	private Dictionary<string, GameObject> unitsDictionary = new Dictionary<string, GameObject>();
	private Dictionary<string, GameObject> structuresDictionary = new Dictionary<string, GameObject>();

	void Awake() {
		tiles.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Tiles"));
		units.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Units"));
		structures.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Structures"));
	
		createDictionary();
		readFile();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void createDictionary() {
		foreach (GameObject tile in tiles) {
			tileDictionary.Add(tile.name, tile);
		}
		
		foreach (GameObject unit in units) {
			unitsDictionary.Add(unit.name, unit);
		}
		
		foreach (GameObject structure in structures) {
			structuresDictionary.Add(structure.name, structure);
		}
	}
	
	void readFile() {
		TextAsset text = (TextAsset)Resources.Load("Levels/" + fileName, typeof(TextAsset));
		
		using (StringReader reader = new StringReader(text.text)) {
			string line;
			string[] words;
			while ((line = reader.ReadLine()) != null) {
				if (line == "-Start Tiles") {
					while ((line = reader.ReadLine()) != null && line != "-End Tiles") {
						words = line.Split(',');	// Splits comma
						field.map.Add (Instantiate(tileDictionary[words[0]],
								new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3])),
						        Quaternion.identity) as GameObject);
						field.map[field.map.Count - 1].GetComponent<HexTile>().field = field;
						for (int x = 4; x < words.Length; x++) {
							// Adds in the neightbor data
							field.map[field.map.Count - 1].GetComponent<HexTile>().neighbors.Add(int.Parse(words[x]));
						}
					}
				}
				
				// new Vector3(float.Parse(field.map[int.Parse(words[1])]), float.Parse(field.map[int.Parse(words[2])]), float.Parse(field.map[int.Parse(words[3])
				
				if (line == "-Start Units") {
					while ((line = reader.ReadLine()) != null && line != "-End Units") {
						words = line.Split(',');	// Splits comma
						GameObject newUnit = (Instantiate(unitsDictionary[words[0]],
					 			field.map[int.Parse(words[1])].transform.position,
						        Quaternion.identity) as GameObject);
						newUnit.transform.position = new Vector3(newUnit.transform.position.x, newUnit.transform.position.y, -0.5f);
						newUnit.GetComponent<Unit>().onTile = field.map[int.Parse(words[1])];
						system.playerUnits.Add (newUnit);
					}
				}
				
				if (line == "-Start Structures") {
					while ((line = reader.ReadLine()) != null && line != "-End Structures") {
						words = line.Split(',');	// Splits comma
						GameObject newStructure = (Instantiate(structuresDictionary[words[0]],
						                                  field.map[int.Parse(words[1])].transform.position,
						                                  Quaternion.identity) as GameObject);
						newStructure.transform.position = new Vector3(newStructure.transform.position.x, newStructure.transform.position.y, -0.5f);
						newStructure.GetComponent<Structure>().onTile = field.map[int.Parse(words[1])];
					}
				}
			}
		}
	}
}
