using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitDeployment : MonoBehaviour {

	public List<GameObject> playerUnits = new List<GameObject>();
	public List<GameObject> deployers = new List<GameObject>();
	public GameObject unitObject;
	public GameObject endTurnButton;
	public GameObject playerController;
	public GameObject unitUI;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		StartCoroutine (showUnits ());
	}
	
	IEnumerator showUnits() {
		animator.SetBool("Close", false);
		yield return new WaitForSeconds(0.4f);
		playerUnits.AddRange(Resources.LoadAll<GameObject>("Prefabs/Game/Units/Player"));
		for (int x = 0; x < playerUnits.Count; x++)
			if (playerUnits[x].name == "0_Placement Slot")
				playerUnits.RemoveAt(x);
		
		deployers.AddRange(GameObject.FindGameObjectsWithTag("UnitDeployment"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void deploy() {
		StartCoroutine (delayUnits ());
	}
	
	IEnumerator delayUnits() {
		animator.SetBool("Close", true);
		yield return new WaitForSeconds(0.4f);
		foreach(GameObject dep in deployers) {
			dep.GetComponent<Deployment>().deployUnits();
		}
		Destroy(gameObject);
		endTurnButton.SetActive(true);
		playerController.SetActive(true);
		unitUI.SetActive(true);
	}
	
	void OnGUI() {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Scroll_Open"))
			playerUnitButtons();
	}
	
	void playerUnitButtons() {
		Event e = Event.current;
		for (int x = 0; x < playerUnits.Count; x++) {
			// Button is replaced with these do allow dragging the object on mousedown instead of mouseup (Just feels better this way)
			Rect rect = new Rect (35 + x*60, Screen.height - 75, 50, 50);
			GUI.DrawTexture(rect, (Texture2D)playerUnits[x].GetComponent<SpriteRenderer>().sprite.texture, ScaleMode.ScaleToFit, true);
			if ((e.type == EventType.MouseDown) && rect.Contains (Event.current.mousePosition)){
				GameObject temp = Instantiate (unitObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity) as GameObject;
				temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -0.5f);
				temp.GetComponent<SpriteRenderer>().sprite = playerUnits[x].GetComponent<SpriteRenderer>().sprite;
				temp.GetComponent<UnitObject>().unit = playerUnits[x];
			}
		}
	}
}
