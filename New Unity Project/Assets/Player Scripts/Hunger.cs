using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hunger : MonoBehaviour {

	float totalHunger = 100f;
	[Range(0,1)]
	[HideInInspector]
	public float currentHunger = 1f;
	public float depletionRate = 0.3f;
	public GameObject DeathPanel;
	public Text HungerText;
	public Image HungerBG;
	bool isAI = false;

	void Awake () {
		if (GetComponent<Brain> ()) {
			isAI = true;
		}
	}

	void Update () {
		if (!isAI) {
			HungerText.text = ("" + Mathf.Round (currentHunger * 100));
			if (currentHunger <= 0) {
				DeathPanel.SetActive (true);
			}
			if (currentHunger < 0.25f) {
				HungerBG.color = new Color (255, 0, 0);
			}
		}

		currentHunger -= (Time.deltaTime / totalHunger) * depletionRate;

		if (currentHunger > 1f) {
			currentHunger = 1f;
		}
	}
}
