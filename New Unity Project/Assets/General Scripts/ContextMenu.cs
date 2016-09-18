using UnityEngine;
using System.Collections;

public class ContextMenu : MonoBehaviour {

	public GameObject ContextSprite;

	void OnMouseDown () {
		if (ContextSprite.activeSelf == false) {
			ContextSprite.SetActive (true);
		} else if (ContextSprite.activeSelf == true) {
			ContextSprite.SetActive (false);
		}
	}
}
