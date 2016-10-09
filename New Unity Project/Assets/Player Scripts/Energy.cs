using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

	float totalEnergy = 100f;
	[Range(0,1)]
	[HideInInspector]
	public float currentEnergy = 1f;
	public float depletion;
	public bool isPregnant;
    public float energyIncrease = 2;
	public GameObject DeathPanel;
	public Text EnergyText;
	public Image EnergyBG;
	bool isAI = false;
	GenerateGrid grid;

	void Awake () {
		grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		if (GetComponent<Brain> ()) {
			isAI = true;
		}
	}
    Vector2 curPos;
    Vector2 lastPos;
	void Update () {
		//is the parent an AI? if so don't change texts
		if (!isAI) {
			EnergyText.text = ("" + Mathf.Round (currentEnergy * 100));
			if (currentEnergy <= 0) {
				DeathPanel.SetActive (true);
			}
			if (currentEnergy < 0.25f) {
				EnergyBG.color = new Color (255, 0, 0);
			}
		}
        curPos = transform.position;
      
        
        TerrainTileValues t;

		if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords((int)transform.position.x, (int)transform.position.y), out t))
		{
			depletion = t.speed;
            if (curPos == lastPos)
            {
                currentEnergy += (Time.deltaTime / totalEnergy) * (depletion * 2);
            }
            else
            {
                currentEnergy -= (Time.deltaTime / totalEnergy) * ((1 / depletion) / 10) * 2;
            }
		}
      
        if (currentEnergy > 1f) {
			currentEnergy = 1f;
		}
        lastPos = curPos;
    }
}