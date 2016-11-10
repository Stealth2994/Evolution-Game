using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MakeBuildingScripto : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("Player").GetComponent<PlayerActions>().StartBuild(); });
	}

}
