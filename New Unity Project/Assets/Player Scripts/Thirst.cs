using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Thirst : MonoBehaviour {

	float totalThirst = 100f;
	[Range(0,1)]
	[HideInInspector]
	public float currentThirst = 1f;
	public float depletionRate = 1f;
	public GameObject DeathPanel;
	public Text ThirstText;
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
			ThirstText.text = ("" + Mathf.Round (currentThirst * 100));
			if (currentThirst <= 0) {
				DeathPanel.SetActive (true);
			}
		}

		currentThirst -= (Time.deltaTime / totalThirst) * depletionRate;

		TerrainTileValues t;

		float code;

		if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords((int)transform.position.x, (int)transform.position.y), out t))
		{
			code = t.code;

			if (code == 1111) {
				currentThirst += (Time.deltaTime / totalThirst) * (depletionRate * 10);
			}
		}

		if (currentThirst > 1f) {
			currentThirst = 1f;
		}
	}
}
