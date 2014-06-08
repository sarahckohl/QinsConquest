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
		if(system.levelEnd){
			if(GameState.level == "Chu") {GameState.chuConquered = true;}
			if(GameState.level == "Han") {GameState.hanConquered = true;}
			if(GameState.level == "Qi") {GameState.qiConquered = true;}
			if(GameState.level == "Wei") {GameState.weiConquered = true;}
			if(GameState.level == "Yan") {GameState.yanConquered = true;}
			if(GameState.level == "Zhao") {GameState.zhaoConquered = true;}
			Application.LoadLevel(1);
		}
	}
}
