using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

	float totalEnergy = 100f;
	[Range(0,1)]
	[HideInInspector]
	public float currentEnergy = 1f;
	public GameObject DeathPanel;
	public Text EnergyText;
	bool isAI = false;
	GenerateGrid grid;

	void Awake () {
		grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		if (GetComponent<Brain> ()) {
			isAI = true;
		}
	}

	void Update () {
		if (!isAI) {
			EnergyText.text = ("" + Mathf.Round (currentEnergy * 100));
			if (currentEnergy <= 0) {
				DeathPanel.SetActive (true);
			}
		}

		TerrainTileValues t;

		float depletion;

		if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords((int)transform.position.x, (int)transform.position.y), out t))
		{
			depletion = t.speed;
			currentEnergy -= (Time.deltaTime / totalEnergy) * ((1 / depletion) / 10) * 2;
		}

		if (currentEnergy > 1f) {
			currentEnergy = 1f;
		}
	}
}