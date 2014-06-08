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
	}
	
	// Update is called once per frame
	void Update () {
		if(level == "Chu" && GameState.chuConquered) {conquered = true;}
		if(level == "Han" && GameState.hanConquered) {conquered = true;}
		if(level == "Qi" && GameState.qiConquered) {conquered = true;}
		if(level == "Wei" && GameState.weiConquered) {conquered = true;}
		if(level == "Yan" && GameState.yanConquered) {conquered = true;}
		if(level == "Zhao" && GameState.zhaoConquered) {conquered = true;}
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
			Application.LoadLevel (2);
			// CameraFade.StartAlphaFade( Color.black, false, 2f, 0.0f, loadLevelOnComplete);
		}
	}
	
	void loadLevelOnComplete() {
		Application.LoadLevel (level);
	}
}
