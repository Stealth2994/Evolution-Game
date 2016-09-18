using UnityEngine;
using System.Collections;

public class EatContext : MonoBehaviour {

	public float nutrition;
	GameObject Player;
	PlayerHunger playerHunger;
	public GameObject ContextSprite;
    GenerateGrid grid;
	// Use this for initialization
	void Start () {
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		Player = GameObject.FindWithTag ("Player");
		playerHunger = Player.GetComponent<PlayerHunger>();
	}

    public void OnMouseDown()
    {
        playerHunger.currentHunger = playerHunger.currentHunger + nutrition;
        ContextSprite.SetActive(false);
  }
}
