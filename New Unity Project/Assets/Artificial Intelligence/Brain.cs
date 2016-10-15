using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Brain : MonoBehaviour {
    public Genes mom;
    public Genes dad;
    public Genes me;
	float energyDepletion;
    private bool inCombat = false;
    private bool needsOrders = true;
    public SurvivalStats stats;
    //first int is the trait from Genes that is top of the list,
    public Dictionary<int, int> priorityList = new Dictionary<int, int>();
    Hunger h;
    Energy r;
    Animator a;
    bool getFood;
    Vector2 target;
    GenerateGrid grid;
    bool getWater;
    bool getRest;
    bool tryBreed;
    float waitTime = 1;
    float tempWait = 0;
    bool firstLoop = true;
	public GameObject zzz;
	public GameObject drinkingSymbol;
	public GameObject eatingSymbol;
	// Use this for initialization
	void Start () {
		energyDepletion = GetComponent<Energy> ().depletion;
        AIS = GameObject.Find("AIS");
        a = transform.FindChild("Player Sprite").GetComponent<Animator>();
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
        h = GetComponent<Hunger>();
       r = GetComponent<Energy>();
        me = GetComponent<Genes>();
        stats = GetComponent<SurvivalStats>();
        me.CreateGenes(mom, dad);
	}
    float tempSpeed;
    public bool wandering = false;
    public string currentPriority = "";
    // Update is called once per frame
    void Update () {
        TerrainTileValues tr;

      

        if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords((int)transform.position.x, (int)transform.position.y), out tr))
        {
            tempSpeed =me.speed * tr.speed;
        }
        else
        {
            tempSpeed = me.speed;
        }


        if (wanderticks > 0)
        {
            wandering = true;
            needsOrders = true;
            wanderticks--;
            Wander();
            return;
        }
        wandering = false;
	    if(needsOrders)
        {
            needsOrders = false;
            List<string> priorities = me.makePriorityList(stats, inCombat);
            currentPriority = priorities[0];
            if(priorities[0] == "needFood")
            {
                getFood = true;
                a.enabled = true;
                wantBreed = false;
            }
            else if (priorities[0] == "needWater")
            {
                getWater = true;
                a.enabled = true;
                wantBreed = false;
            }
            else if (priorities[0] == "needBreed")
            {
                tryBreed = true;
                a.enabled = true;
                wantBreed = true;
            }
            else if (priorities[0] == "needRest")
            {
                getRest = true;
                a.enabled = false;
                wantBreed = false;
            }
        }
        if(getFood)
        {
            if(firstLoop) {
                List<Vector2> targets = new List<Vector2>();
                firstLoop = false;
                for (int x = (int)transform.position.x - me.sight; x < transform.position.x + me.sight; x++)
                {
                    for (int y = (int)transform.position.y - me.sight; y < transform.position.y + me.sight; y++)
                    {
                        TerrainTileValues t;
                       // Debug.Log(GenerateGrid.foodList.Count);
                        if(GenerateGrid.foodList.TryGetValue(new GenerateGrid.coords(x, y), out t))
                        {
                            if (t.GetComponent<FoodValues>())
                            {
                                targets.Add(new Vector2(x, y));
                            }
                        }
                        
                       
                    }
                }
                if (targets.Count > 0)
                {
                    float closest = 100000;
                    Vector2 closet = Vector2.zero;
                    foreach (Vector2 t in targets)
                    {
                        if (Vector2.Distance(transform.position, t) < closest)
                        {
                            closet = t;
                        }
                    }
                    target = closet;
                }
                else
                {
                    wanderticks = Random.Range(0, 60);
                    wanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                    firstLoop = true;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * tempSpeed);
                if (Vector2.Distance(transform.position, target) < 0.01)
                {
                    tempWait += Time.deltaTime;
					eatingSymbol.SetActive (true);
                    if(tempWait > waitTime) {
						eatingSymbol.SetActive (false);
                        tempWait = 0;
                        if (!GenerateGrid.removeFoodList.ContainsKey(new GenerateGrid.coords((int)target.x, (int)target.y)))
                        {
                            if (GenerateGrid.createdFoods.ContainsKey(new GenerateGrid.coords((int)target.x, (int)target.y)))
                            {
                                GameObject t;
                                GenerateGrid.createdFoods.TryGetValue(new GenerateGrid.coords((int)target.x, (int)target.y), out t);
                               h.currentHunger += t.transform.FindChild("ContextMenu").FindChild("EatButton").GetComponent<EatContext>().nutrition * 100;
                                GenerateGrid.removeFoodList.Add(new GenerateGrid.coords((int)target.x, (int)target.y), t.gameObject);

                            }
                            else
                            {
                                TerrainTileValues t;
                                if (GenerateGrid.foodList.TryGetValue(new GenerateGrid.coords((int)target.x, (int)target.y), out t))
                                {
                                    Debug.Log("An AI found food!");
                                    h.currentHunger = h.currentHunger + 20;
                                    GenerateGrid.foodList.Remove(new GenerateGrid.coords((int)target.x, (int)target.y));
                                }
                            }
                            getFood = false;
                            firstLoop = true;
                            needsOrders = true;
                        }
                    }
                    else
                    {
                    }
                  
                }
            }
        }
        else if(getWater)
		{
            if (firstLoop)
            {
                List<Vector2> targets = new List<Vector2>();
                firstLoop = false;
                for (int x = (int)transform.position.x - me.sight; x < transform.position.x + me.sight; x++)
                {
                    for (int y = (int)transform.position.y - me.sight; y < transform.position.y + me.sight; y++)
                    {
                        TerrainTileValues t;
                        if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords(x, y), out t))
                        {
                            if (t.code == 1111)
                            {
                                targets.Add(new Vector2(x, y));
                            }
                        }
                    }
                }
                if(targets.Count > 0)
                {
                    float closest = 100000;
                    Vector2 closet = Vector2.zero;
                  foreach(Vector2 t in targets)
                    {
                        if(Vector2.Distance(transform.position,t) < closest)
                        {
                            closet = t;
                        }
                    }
                    target = closet;
                }
                else
                {
                    wanderticks = Random.Range(0, 60);
                    wanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                    firstLoop = true;
                }
               
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * tempSpeed);
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
			zzz.SetActive (true);
            tempWait += Time.deltaTime;
            if(tempWait > 5)
            {
				zzz.SetActive (false);
                getRest = false;
                tempWait = 0;
                needsOrders = true;
            }
           
        }
        else if(tryBreed)
        {
            if (firstLoop)
            {
                firstLoop = false;
                foreach (Brain b in CreateAIS.aiList)
                {
                    if (b.wantBreed && Vector2.Distance(transform.position, b.transform.position) < me.sight)
                    {
                        if (me.gender != b.me.gender)
                        {
                            if (b.breedTarget != null)
                            {
                                if (me.goodLooks > b.breedTarget.me.goodLooks)
                                {
                                    breedTarget = b;
                                    b.breedTarget = GetComponent<Brain>();
                                    break;
                                }
                            }
                            else
                            {
                                breedTarget = b;
                                b.breedTarget = GetComponent<Brain>();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                if(breedTarget == null)
                {
                    wanderticks = Random.Range(0, 60);
                    wanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                    firstLoop = true;
                    tryBreed = false;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, breedTarget.transform.position, Time.deltaTime * tempSpeed);
                    if(Vector2.Distance(transform.position,breedTarget.transform.position) < 1)
                    {
                        wanderticks = Random.Range(60, 120);
                        wanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                        firstLoop = true;
                        tryBreed = false;
                        breedTarget = null;
                        if (me.gender == 1)
                        {
							BirthCountdown ();
                        }
                    }
                }
            }
            
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
    }

	public void BirthCountdown () {
		Debug.Log ("kid in 1 minute kchow");
		r.isPregnant = true;
		r.energyIncrease = 0.75f;
		Invoke ("HaveKid", 60);
	}
	public void HaveKid() {
		Debug.Log ("female had kid kchow");
		r.isPregnant = false;
		r.energyIncrease = 2;
		GameObject g = Instantiate(AI, transform.position, Quaternion.identity) as GameObject;
		g.transform.parent = AIS.transform;
		CreateAIS.aiList.Add(g.GetComponent<Brain>());
	}

    public GameObject AI;
    public Brain breedTarget;
    public bool wantBreed = false;
    GameObject AIS;
    Vector2 curPos;
    Vector2 lastPos;
    int wanderticks;
    Vector2 wanderDirection;
    public void Wander()
    {
  
                transform.Translate(wanderDirection * Time.deltaTime * tempSpeed);


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
        
    }
}
