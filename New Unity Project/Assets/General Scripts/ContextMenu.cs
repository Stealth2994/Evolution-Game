using UnityEngine;
using System.Collections;

public class ContextMenu : MonoBehaviour {

	public GameObject ContextSprite;
	GenerateGrid grid;

	void Start () {
		grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
	}

    void OnMouseDown()
    {

        if (ContextSprite.activeSelf == false)
        {
            ContextSprite.SetActive(true);
        }
        else if (ContextSprite.activeSelf == true)
        {
            ContextSprite.SetActive(false);
        }
    }
}
