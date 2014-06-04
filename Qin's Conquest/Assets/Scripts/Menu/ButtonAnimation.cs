using UnityEngine;
using System.Collections;

public class ButtonAnimation : MonoBehaviour {

	public int level;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	void OnMouseOver() {
		if(!CameraFade.fading) {
			animator.SetBool("MouseOver", true);
		}
	}
	
	void OnMouseExit() {
		if(!CameraFade.fading) {
			animator.SetBool("MouseOver", false);
		}
	}
	
	void OnMouseUp() {
		animator.SetBool("MouseOver", false);
		if(!CameraFade.fading) {
			CameraFade.StartAlphaFade( Color.black, false, 2f, 0.0f, loadLevelOnComplete);
		}
	}
	
	void loadLevelOnComplete() {
		Application.LoadLevel (level);
	}
}
