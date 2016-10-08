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
    Hunger h;
    bool getFood;
    Vector2 target;
    bool getWater;
    bool getRest;
    bool tryBreed;
    bool firstLoop = true;
	// Use this for initialization
	void Start () {
        h = GetComponent<Hunger>();
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
                       // Debug.Log(GenerateGrid.foodList.Count);
                        if(GenerateGrid.foodList.TryGetValue(new GenerateGrid.coords(x, y), out t))
                        {
                            if (t.food)
                            {
                                Debug.Log("well then");
                                target = new Vector2(x, y);
                                return;
                            }
                        }
                        
                       
                    }
                }
                Debug.Log("uhhhhh, wheres da food");
                wanderticks = Random.Range(0, 250);
                wanderDirection = Random.Range(1, 4);
                firstLoop = true;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * me.speed);
                if (Vector2.Distance(transform.position, target) < 0.01)
                {
                    if (!GenerateGrid.removeFoodList.ContainsKey(new GenerateGrid.coords((int)target.x, (int)target.y)))
                    {
                        if (GenerateGrid.createdFoods.ContainsKey(new GenerateGrid.coords((int)target.x, (int)target.y)))
                        {
                            GameObject t;
                            GenerateGrid.createdFoods.TryGetValue(new GenerateGrid.coords((int)target.x, (int)target.y), out t);
                            stats.hunger = stats.hunger + t.transform.FindChild("ContextMenu").FindChild("EatButton").GetComponent<EatContext>().nutrition;
                            GenerateGrid.removeFoodList.Add(new GenerateGrid.coords((int)target.x, (int)target.y), t.gameObject);
                        }
                        else
                        {
                            TerrainTileValues t;
                            if (GenerateGrid.foodList.TryGetValue(new GenerateGrid.coords((int)target.x, (int)target.y), out t))
                            {
                                Debug.Log("YAA BOIZ");
                                h.currentHunger = h.currentHunger + 20;
                                GenerateGrid.foodList.Remove(new GenerateGrid.coords((int)target.x, (int)target.y));
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("wat theres no food here - ai");
                    }
                    
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
                        if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords(x, y), out t))
                        {
                            if (t.code == 1111 || t.code == 1112)
                            {
                                target = new Vector2(x, y);
                                return;
                            }
                        }
                    }
                }
                Debug.Log("uhhhhh, wheres da water");
                wanderticks = Random.Range(0, 250);
                wanderDirection = Random.Range(1, 4);
                firstLoop = true;
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
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
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
                transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        }
    }
}
