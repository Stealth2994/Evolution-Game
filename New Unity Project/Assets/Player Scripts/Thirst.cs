using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Thirst : MonoBehaviour {

	float totalThirst = 100f;
	[Range(0,1)]
	[HideInInspector]
	public float currentThirst = 1f;
	public float depletionRate = 0.5f;
	public GameObject DeathPanel;
	public Text ThirstText;
	public Image ThirstBG;
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
			if (currentThirst < 0.25f) {
				ThirstBG.color = new Color (255, 0, 0);
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
