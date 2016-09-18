using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour {

	float totalHunger = 100f;
	[Range(0,1)]
	[HideInInspector]
	public float currentHunger = 1f;
	public float depletionRate = 1f;
	public Text HungerText;

	void Update () {
		currentHunger -= (Time.deltaTime / totalHunger) * depletionRate;
		HungerText.text = ("H: " + Mathf.Round (currentHunger * 100));

		if (currentHunger > 1f) {
			currentHunger = 1f;
		}
	}
}
