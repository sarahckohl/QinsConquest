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
		deployer.deploy();
		endTurnButton.SetActive(true);
	}
}
