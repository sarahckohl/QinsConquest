using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GameObject title;

	private Animator animator;

	// Use this for initialization
	void Awake () {
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void openUp() {
		title.SetActive(false);
		animator.SetBool("MouseOver", true);
		audio.Play ();
	}
	
	void OnMouseDown() {
		StartCoroutine(CloseCoroutine());
	}
	
	IEnumerator CloseCoroutine() {
		animator.SetBool("MouseOver", false);
		audio.Play ();
		title.SetActive(true);
		yield return new WaitForSeconds(0.15f);
		this.gameObject.SetActive(false);
	}
}
