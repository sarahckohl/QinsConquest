using UnityEngine;
using System.Collections;

public class StartMissonButton : MonoBehaviour {

	private Animator animator;
	public UnitDeployment deployer;
	public GameObject endTurnButton;
	
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	void OnMouseOver() {
		animator.SetBool("MouseOver", true);
		audio.Play ();
	}
	
	void OnMouseExit() {
		animator.SetBool("MouseOver", false);
		audio.Play ();
	}
	
	void OnMouseUp() {
		animator.SetBool("MouseOver", false);
		audio.Play ();
		deployer.deploy();
		endTurnButton.SetActive(true);
	}
}
