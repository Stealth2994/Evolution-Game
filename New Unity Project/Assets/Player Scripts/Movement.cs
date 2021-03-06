﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
    GenerateGrid grid;
    public float nutrition;
	float revealOffset = 1;
	bool isRotating = false;
	float currentDir;
	float headingDir;
	float minColour = 0f;
	public SpriteRenderer Loader;
	float maxColour = 1f;
    public Hunger playerHunger;
    public bool doit = false;
	float posY;
	float posX;
    GameObject Player;
	public SpriteRenderer PlayerSpriteRenderer;
	public SpriteRenderer PlayerBodySpriteR;
	public Animator PlayerAnimator;
	public GameObject PlayerSprite;
	public float movementSpeed = 5;
	Rigidbody2D rb;
    public float eatTime;
    float tempTime = 0;
    public GameObject gg;
    public Transform target;

	Genes mom;
	Genes dad;

    void Start () {
		GetComponent<Genes> ().CreateGenes (mom, dad);
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
        Player = GameObject.FindWithTag("Player");
		PlayerBodySpriteR.color = new Color (Random.Range(minColour,maxColour), Random.Range(minColour,maxColour), Random.Range(minColour,maxColour));
		PlayerAnimator.Play ("PlayerAnimation");
		rb = this.GetComponent<Rigidbody2D> ();
	}
    
	void FixedUpdate () {
		TerrainTileValues t;

        float tempSpeed;

        if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords((int)transform.position.x, (int)transform.position.y), out t))
        {
         tempSpeed   = movementSpeed * t.speed;
        }
        else
        {
            tempSpeed = movementSpeed;
        }

   


        if (doit)
        {
			Loader.enabled = true;
			revealOffset = revealOffset - 0.02f;
			Loader.material.SetFloat ("_Cutoff", revealOffset);
            tempTime += Time.deltaTime;
            if (tempTime > eatTime)
            {
                
                if (!GenerateGrid.removeFoodList.ContainsKey(new GenerateGrid.coords((int)gg.transform.position.x, (int)gg.transform.position.y)))
                {
                    playerHunger.currentHunger = playerHunger.currentHunger + nutrition;
                    GenerateGrid.removeFoodList.Add(new GenerateGrid.coords((int)gg.transform.position.x, (int)gg.transform.position.y), gg);
                    tempTime = 0;
                    doit = false;
					Loader.enabled = false;
					revealOffset = 1;
                }


            }


        }

        else
        {



            if (transform.position.x > grid.length)
            {
                transform.position = new Vector3(grid.length, transform.position.y, transform.position.z);
            }
            if (transform.position.y > grid.width - 1)
            {
                transform.position = new Vector3(transform.position.x, grid.width - 1, transform.position.z);
            }
            if (transform.position.x < 1)
            {
                transform.position = new Vector3(1, transform.position.y, transform.position.z);
            }
            if (transform.position.y < 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }

            posY = Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical"));
            posX = Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal"));
            transform.Translate(CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * tempSpeed, CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * tempSpeed, 0);
            transform.position += Vector3.ClampMagnitude(new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * tempSpeed, CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * tempSpeed), tempSpeed) * Time.deltaTime;



            if (posY > 0f || posX > 0f || isRotating)
            {
                PlayerAnimator.Play("PlayerAnimation");
            }
            else
            {
                PlayerAnimator.Play("IdleAnimation");
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            //rotation
            if (!isRotating)
            {
                headingDir = Mathf.Atan2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
                if (CrossPlatformInputManager.GetAxis("Horizontal") == 0 && CrossPlatformInputManager.GetAxis("Vertical") == 0)
                {
                    PlayerSprite.transform.rotation = Quaternion.Inverse(Quaternion.Euler(0f, 0f, currentDir * Mathf.Rad2Deg));
                }
                else
                {
                    currentDir = headingDir;
                    PlayerSprite.transform.rotation = Quaternion.Inverse(Quaternion.Euler(0f, 0f, headingDir * Mathf.Rad2Deg));
                }
            }
            else
            {
                //	Debug.Log ("<size=25> dingus </size>");
            }
        }
	}
}
