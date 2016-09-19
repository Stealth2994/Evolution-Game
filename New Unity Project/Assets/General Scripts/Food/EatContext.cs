﻿using UnityEngine;
using System.Collections;

public class EatContext : MonoBehaviour {

    public float nutrition;
    GameObject Player;
	
    Movement m;
    public GameObject ContextSprite;
    GenerateGrid grid;
	// Use this for initialization
	void Start () {
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		Player = GameObject.FindWithTag ("Player");
        m = Player.GetComponent<Movement>();
		
	}
    public void OnMouseDown()
    {
        m.nutrition = nutrition;
        m.target = new Vector3((int)transform.position.x, (int)transform.position.y, -0.5f);
        m.doit = true;
       
        ContextSprite.SetActive(false);
    }
    void Update()
    {
      
     
    }      
    
}
