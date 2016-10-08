using UnityEngine;
using System.Collections;

public class AIContext : MonoBehaviour {
	public GameObject ContextMenu;
	public void OnMouseDown()
	{
		if (ContextMenu.activeSelf == false) {

			Time.timeScale = 0.1f;
			ContextMenu.SetActive (true);

		} else if (ContextMenu.activeSelf == true) {
			Time.timeScale = 1f;
			ContextMenu.SetActive (false);
		}
	}
}
