using UnityEngine;
using System.Collections;

public class EatContext : MonoBehaviour {

	public int nutrition;
	GameObject Player;
	PlayerHunger playerHunger;
	public GameObject ContextSprite;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag ("Player");
		playerHunger = Player.GetComponent<PlayerHunger>();
	}

	public void OnMouseDown () {
		playerHunger.currentHunger = playerHunger.currentHunger + nutrition;
		ContextSprite.SetActive (false);
	}

}
