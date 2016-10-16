using UnityEngine;
using System.Collections;

public class ContextMenu : MonoBehaviour {

	public GameObject ContextSprite;
	GenerateGrid grid;
    GameObject Player;
    float range;
    public ContextBase eater;
	void Start () {
		grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
        eater = transform.Find("ContextMenu").Find("EatButton").GetComponent<ContextBase>();
        Player = GameObject.FindWithTag("Player");
        eater.Awake();
    }

    void OnMouseDown()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Player.transform.position.x - (transform.position.x), 2) + Mathf.Pow(Player.transform.position.y - (transform.position.y), 2));
        if(distance > eater.range)
        {
            eater.GreyOut();
        }
        else
        {
            eater.UnGreyOut();          
        }
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
