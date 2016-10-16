using UnityEngine;
using System.Collections;

public class ContextBase : MonoBehaviour {

    public float nutrition;
    public GameObject Player;
    public bool inRange = false;
    public BoxCollider2D box;
    public SpriteRenderer sprite;
    public Movement m;
    public int range = 3;
    public GameObject ContextSprite;
    public GenerateGrid grid;
    float eatTime = 1;
    // Use this for initialization
    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
        Player = GameObject.FindWithTag("Player");
        m = Player.GetComponent<Movement>();
        m.playerHunger = Player.GetComponent<Hunger>();
        m.eatTime = eatTime;
    }
    public void GreyOut()
    {
        box.enabled = false;
        sprite.color = Color.grey;
    }
    public void UnGreyOut()
    {
        box.enabled = true;
        sprite.color = Color.white;
    }
}
