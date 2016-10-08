using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Brain : MonoBehaviour {
    public Genes mom;
    public Genes dad;
    public Genes me;
    private bool inCombat = false;
    private bool needsOrders = true;
    public SurvivalStats stats;
    //first int is the trait from Genes that is top of the list,
    public Dictionary<int, int> priorityList = new Dictionary<int, int>();

    bool getFood;
    Vector2 target;
    bool getWater;
    bool getRest;
    bool tryBreed;
    bool firstLoop = true;
	// Use this for initialization
	void Start () {
        me = GetComponent<Genes>();
        stats = GetComponent<SurvivalStats>();
        me.CreateGenes(mom, dad);
	}
	
	// Update is called once per frame
	void Update () {
        if (wanderticks > 0)
        {
            wanderticks--;
            Wander();
            return;
        }
	    if(needsOrders)
        {
            needsOrders = false;
            List<string> priorities = me.makePriorityList(stats, inCombat);
            if(priorities[0] == "needFood")
            {
                getFood = true;
            }
            else if (priorities[0] == "needWater")
            {
                getWater = true;
            }
            else if (priorities[0] == "needBreed")
            {
                getRest = true;
            }
            else if (priorities[0] == "needRest")
            {
                tryBreed = true;
            }
        }
        if(getFood)
        {
            if(firstLoop) {
                firstLoop = false;
                for (int x = (int)transform.position.x - me.sight; x < transform.position.x + me.sight; x++)
                {
                    for (int y = (int)transform.position.y - me.sight; y < transform.position.y + me.sight; y++)
                    {
                        TerrainTileValues t;
                        GenerateGrid.grid.TryGetValue(new GenerateGrid.coords(x, y), out t);
                        if (t.food)
                        {
                            target = new Vector2(x, y);
                            break;
                        }
                    }
                }
                wanderticks = Random.Range(0, 250);
                wanderDirection = Random.Range(1, 4);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * me.speed);
                if (Vector2.Distance(transform.position, target) < 0.01)
                {
                    getFood = false;
                    firstLoop = true;
                    needsOrders = true;
                }
            }
        }
        else if(getWater)
        {
            if (firstLoop)
            {
                firstLoop = false;
                for (int x = (int)transform.position.x - me.sight; x < transform.position.x + me.sight; x++)
                {
                    for (int y = (int)transform.position.y - me.sight; y < transform.position.y + me.sight; y++)
                    {
                        TerrainTileValues t;
                        GenerateGrid.grid.TryGetValue(new GenerateGrid.coords(x, y), out t);
                        if (t.code == 1111 || t.code == 1112)
                        {
                            target = new Vector2(x, y);
                            break;
                        }
                    }
                }
                wanderticks = Random.Range(0, 250);
                wanderDirection = Random.Range(1, 4);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * me.speed);
                if (Vector2.Distance(transform.position, target) < 0.01)
                {
                    getWater = false;
                    firstLoop = true;
                    needsOrders = true;
                }
            }
        }
        else if(getRest)
        {

        }
        else if(tryBreed)
        {

        }
	}
    int wanderticks;
    int wanderDirection;
    public void Wander()
    {
        switch(wanderDirection)
        {
            case 1: transform.Translate(Vector2.up * Time.deltaTime * me.speed);
                break;
            case 2: transform.Translate(Vector2.right * Time.deltaTime * me.speed);
                break;
            case 3: transform.Translate(Vector2.down * Time.deltaTime * me.speed);
                break;
            case 4: transform.Translate(Vector2.left * Time.deltaTime * me.speed);
                break;

        }
    }
}
