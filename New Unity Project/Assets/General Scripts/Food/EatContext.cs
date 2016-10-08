using UnityEngine;
using System.Collections;

public class EatContext : MonoBehaviour {
    
    public float nutrition;
    GameObject Player;
    public bool inRange = false;
    BoxCollider2D box;
    SpriteRenderer sprite;
    Movement m;
    public int range = 3;
    public GameObject ContextSprite;
    GenerateGrid grid;
    float eatTime = 1;
	// Use this for initialization
	public void Awake () {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		Player = GameObject.FindWithTag ("Player");
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
    public void OnMouseDown()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Player.transform.position.x - (transform.position.x + 3), 2) + Mathf.Pow(Player.transform.position.y - (transform.position.y - 1), 2));
        Debug.Log(distance);
        if (distance < range)
        {
            
            m.target = transform;
            m.doit = true;
            Debug.Log(transform.parent.transform.parent.gameObject);
            m.nutrition = nutrition;
            m.gg = transform.parent.transform.parent.gameObject;
        }
        ContextSprite.SetActive(false);
    }


 
}
