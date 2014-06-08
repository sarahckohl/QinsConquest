using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

	public static string level;
	public static Dictionary<string, bool> stateDictionary = new Dictionary<string, bool>();

	// Use this for initialization
	void Awake () {
		if (!stateDictionary.ContainsKey("Qi"))
			stateDictionary.Add("Qi", false);
			
		if (!stateDictionary.ContainsKey("Han"))
			stateDictionary.Add("Han", false);
			
		if (!stateDictionary.ContainsKey("Wei"))
			stateDictionary.Add("Wei", false);
			
		if (!stateDictionary.ContainsKey("Chu"))
			stateDictionary.Add("Chu", false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
