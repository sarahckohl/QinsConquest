using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	
	public int level;
	public float widthOffset = 75, heightOffset = 50;
	public float width = 150, height = 75;
	// public Texture2D buttonSprite;
	public string buttonText;
	
	void OnGUI() {
		GUI.depth = -5;
		if (GUI.Button (new Rect (Screen.width / 2 - widthOffset, Screen.height / 2 + heightOffset, width, height), buttonText)) {
			if(!CameraFade.fading) {
				CameraFade.StartAlphaFade( Color.black, false, 2f, 0.0f, loadLevelOnComplete);
			//	playButtonSound();
			}
		}
	}
	
	void loadLevelOnComplete() {
		Application.LoadLevel (level);
	}
	
	void playButtonSound()
	{
		GameObject buttonSound = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Sound"));
		buttonSound.audio.clip = Resources.Load<AudioClip>("Audio/SFX/menu_button");
		buttonSound.audio.loop = false;
		buttonSound.audio.Play();
	}
}