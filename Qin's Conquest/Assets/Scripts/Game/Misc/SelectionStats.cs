using UnityEngine;
using System.Collections;

public class SelectionStats : MonoBehaviour {

	public SpriteRenderer unitSprite;
	
	public GUIText health;
	public GUIText attack;
	public GUIText defense;
	public GUIText movement;
	public GUIText attackRange;
	
	private Animator animator;
	
	
	void Start() {
		animator = this.GetComponent<Animator>();
		deSelection ();
	}
	
	public void setSelection(GameObject uni) {
		StartCoroutine(setSelectionCoroutine(uni));
	}
	
	public void deSelection() {
		unitSprite.sprite = null;
		health.text = "";
		attack.text = "";
		defense.text = "";
		movement.text = "";
		attackRange.text = "";
		animator.SetBool("Close", true);
	}
	
	IEnumerator setSelectionCoroutine(GameObject uni) {
		animator.SetBool("Close", false);
		yield return new WaitForSeconds(0.4f);
		
		// A check to see if player deselected unit while we were yielding
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Scroll_Open")) {
			unitSprite.sprite = uni.GetComponent<SpriteRenderer>().sprite;
			if (uni.GetComponent<Unit>() != null) 
				settingUnit (uni.GetComponent<Unit>());
			else if (uni.GetComponent<Structure>() != null)
				settingStruct(uni.GetComponent<Structure>());
			else 
				Debug.Log ("Shits Broken");
		}
	}
	
	void settingUnit(Unit unit) {
		health.text = "Health: " + unit.health;
		attack.text = "Attack: " + unit.attackVal;
		defense.text = "Defense: " + unit.defenseVal;
		movement.text = "Movement:" + unit.movement;
		attackRange.text = "Range: " + unit.attackRange;
	}
	
	void settingStruct(Structure struc) {
		health.text = "Health: " + struc.health;
	}
}
