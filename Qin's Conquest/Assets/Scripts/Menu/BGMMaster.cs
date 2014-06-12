using UnityEngine;
using System.Collections;

public class BGMMaster : MonoBehaviour {

	public AudioClip mainMusic;
	public AudioClip battleMusic;

	void Awake() {
		DontDestroyOnLoad(gameObject);
		mainMusic = Resources.Load <AudioClip>("Audio/BGM/TitleTheme");
		battleMusic = Resources.Load<AudioClip>("Audio/BGM/Battle");
		audio.loop = true;
		audio.clip = mainMusic;
		audio.Play();
	}
	
	void OnLevelWasLoaded(int level) {
		if (level == 2) {
			audio.Stop();
			audio.clip = battleMusic;
			audio.Play();
		} else {
			if (audio.clip != mainMusic) {
				audio.Stop();
				audio.clip = mainMusic;
				audio.Play();
			}
		}
	}
}
