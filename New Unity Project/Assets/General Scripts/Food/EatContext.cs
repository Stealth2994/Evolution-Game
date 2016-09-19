using UnityEngine;
using System.Collections;

public class EatContext : MonoBehaviour {

	public float nutrition;
	GameObject Player;
	PlayerHunger playerHunger;
    Movement m;
	public GameObject ContextSprite;
    GenerateGrid grid;
	// Use this for initialization
	void Start () {
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		Player = GameObject.FindWithTag ("Player");
        m = Player.GetComponent<Movement>();
		playerHunger = Player.GetComponent<PlayerHunger>();
	}
    public void OnMouseDown()
    {

        m.target = gameObject;
        m.doit = true;
        playerHunger.currentHunger = playerHunger.currentHunger + nutrition;
        ContextSprite.SetActive(false);
    }
    void Update()
    {
      
     
    }      
    
}
