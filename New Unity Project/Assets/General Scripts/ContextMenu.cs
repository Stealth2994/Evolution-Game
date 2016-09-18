using UnityEngine;
using System.Collections;

public class ContextMenu : MonoBehaviour {

	public GameObject ContextSprite;
	GenerateGrid grid;

	void Start () {
		grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
	}

	void OnMouseDown () {
		if (ContextSprite.activeSelf == false) {
			ContextSprite.SetActive (true);
		} else if (ContextSprite.activeSelf == true) {
			ContextSprite.SetActive (false);
		}
			
		if(ContextSprite.transform.position.x > grid.length) {
			ContextSprite.transform.position = new Vector3(grid.length - 2, ContextSprite.transform.position.y, ContextSprite.transform.position.z);
		}
		if (ContextSprite.transform.position.y > grid.length) {
			ContextSprite.transform.position = new Vector3(ContextSprite.transform.position.x,grid.length-2, ContextSprite.transform.position.z);
		}
		if (ContextSprite.transform.position.x < 9) {
			ContextSprite.transform.position = new Vector3(9, ContextSprite.transform.position.y, ContextSprite.transform.position.z);
		}
		if (ContextSprite.transform.position.y < 5) {
			ContextSprite.transform.position = new Vector3(ContextSprite.transform.position.x, 5, ContextSprite.transform.position.z);
		}
	}
}
