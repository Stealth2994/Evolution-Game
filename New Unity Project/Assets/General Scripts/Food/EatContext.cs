using UnityEngine;
using System.Collections;

public class EatContext : MonoBehaviour {
    
    public float nutrition;
    GameObject Player;
    
    
    Movement m;
    public GameObject ContextSprite;
    GenerateGrid grid;
    float eatTime = 1;
	// Use this for initialization
	void Start () {
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		Player = GameObject.FindWithTag ("Player");
        m = Player.GetComponent<Movement>();
        m.playerHunger = Player.GetComponent<PlayerHunger>();
        m.eatTime = eatTime;
    }
    public void OnMouseDown()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Player.transform.position.x - (transform.position.x + 3), 2) + Mathf.Pow(Player.transform.position.y - (transform.position.y - 1), 2));
        Debug.Log(distance);
       
        if (distance < 3)
        {
            
            m.target = transform;
            m.doit = true;
            Debug.Log(transform.parent.transform.parent.gameObject);
            m.gg = transform.parent.transform.parent.gameObject;
        }
        ContextSprite.SetActive(false);
    }


 
}
