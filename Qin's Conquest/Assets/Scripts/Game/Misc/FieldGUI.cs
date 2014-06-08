using UnityEngine;
using System.Collections;

public class FieldGUI : MonoBehaviour {
	public TurnSystem system;
	private Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	void OnMouseOver() {
		animator.SetBool("MouseOver", true);
	}
	
	void OnMouseExit() {
		animator.SetBool("MouseOver", false);
	}
	
	void OnMouseUp() {
		animator.SetBool("MouseOver", false);
		system.turnEnd = true;
	}
}
