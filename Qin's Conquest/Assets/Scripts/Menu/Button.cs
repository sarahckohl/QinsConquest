using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	
	public int level;
	public float widthOffset = 75, heightOffset = 50;
	public float width = 150, height = 75;
	public float xposition;
	public float yposition;
	// public Texture2D buttonSprite;
	public string buttonText;

	void Awake(){
		xposition = gameObject.transform.position.x *300;
		yposition = gameObject.transform.position.y *300;
		buttonText = gameObject.name;
	
	}
	
	void OnGUI() {
		GUI.depth = -5;
		if (GUI.Button (new Rect (xposition, yposition, width, height), buttonText)) {
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