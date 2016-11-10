using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Draggable : MonoBehaviour {

	GameObject hbArea;
	bool isInHotbar;
	public GameObject thisItem;

	public void Start () {
		hbArea = GameObject.FindWithTag ("Hotbar");
		Debug.Log (hbArea.transform.position);
	}

	public void DragButton () {
		transform.position = Input.mousePosition;
		if (Input.mousePosition.x < hbArea.transform.position.x && Input.mousePosition.y > hbArea.transform.position.y) {
			Debug.Log ("yay");
			isInHotbar = true;
		} else {
			isInHotbar = false;
		}
	}
	public void DropButton () {
		Debug.Log ("dropped");
		if (isInHotbar) {
			transform.parent = hbArea.transform;
		}
	}
	public void OnClickIcon () {

		GameObject Player = GameObject.FindWithTag ("Player");

		Instantiate (thisItem, new Vector3(Player.transform.position.x, Player.transform.position.y + 1f, 0f), new Quaternion(-180f, 0f, 0f, 0f));

	}
}
