using UnityEngine;
using System.Collections;

public class LevelSelectButtons : MonoBehaviour {

	public string level;
	
	public bool conquered = false;
	
	public Sprite original;
	public Sprite highlight;
	public Sprite takenOver;
	
	private SpriteRenderer rend;
	
	void Start() {
		rend = GetComponent<SpriteRenderer>();
		conquered = GameState.stateDictionary[level];
	}
	
	// Update is called once per frame
	void Update () {
		if (rend.sprite != takenOver && conquered) {
			rend.sprite = takenOver;
		}
	}
	
	void OnMouseOver() {
		if(!CameraFade.fading && !conquered) {
			rend.sprite = highlight;
		}
	}
	
	void OnMouseExit() {
		if(!CameraFade.fading && !conquered) {
			rend.sprite = original;
		}
	}
	
	void OnMouseUp() {
		if(!CameraFade.fading && !conquered) {
			rend.sprite = original;
			GameState.level = level;
			audio.Play ();
			CameraFade.StartAlphaFade( Color.black, false, 2f, 0.0f, loadLevelOnComplete);
		}
	}
	
	void loadLevelOnComplete() {
		Application.LoadLevel (2);
	}
}
