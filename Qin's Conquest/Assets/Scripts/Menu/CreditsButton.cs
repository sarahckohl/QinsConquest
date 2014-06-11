using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour {

	public GameObject credits;

	private Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	void OnMouseEnter() {
		animator.SetBool("MouseOver", true);
		audio.Play ();
	}
	
	void OnMouseExit() {
		animator.SetBool("MouseOver", false);
		audio.Play ();
	}
	
	void OnMouseUp() {
		animator.SetBool("MouseOver", false);
		credits.SetActive(true);
		credits.GetComponent<Credits>().openUp();
	}
}
