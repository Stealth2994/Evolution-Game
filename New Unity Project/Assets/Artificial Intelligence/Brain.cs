using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Brain : MonoBehaviour {
    public Genes mom;
    public Genes dad;
    public Genes me;
	float energyDepletion;
    private bool inCombat = false;
    public bool needsOrders = true;
    public SurvivalStats stats;
    //first int is the trait from Genes that is top of the list,
    public Dictionary<int, int> priorityList = new Dictionary<int, int>();
    Hunger h;
    Energy r;
    Animator a;
    bool getFood;
    public Vector2 target;
    GenerateGrid grid;
    bool getWater;
    bool getRest;
    bool tryBreed;
    float waitTime = 1;
    float tempWait = 0;
    bool firstLoop = true;
    public static List<Vector2> sortOrder = new List<Vector2>();
	public GameObject zzz;
	public GameObject drinkingSymbol;
	public GameObject eatingSymbol;

    // Use this for initialization
    void Start()
    {
        energyDepletion = GetComponent<Energy>().depletion;
        AIS = GameObject.Find("AIS");
        a = transform.FindChild("Player Sprite").GetComponent<Animator>();
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
        h = GetComponent<Hunger>();
        r = GetComponent<Energy>();
        me = GetComponent<Genes>();
        stats = GetComponent<SurvivalStats>();
        me.CreateGenes(mom, dad);
        StartCoroutine(UpdateAI());

}
    //Call this once for the AIS so they work :)
    public static void MakeSortList()
    {
        for (int x = -20; x <= 20; x++)
        {
            for (int y = -20; y <= 20; y++)
            {
                sortOrder.Add(new Vector2(x, y));
            }
        }
        List<Vector2> afterSortOrder = new List<Vector2>();
        while (sortOrder.Count > 0)
        {
            float closest = 200;
            Vector2 closet = new Vector2(200, 200);
            foreach (Vector2 v in sortOrder)
            {
                if (Vector2.Distance(Vector2.zero, v) < Vector2.Distance(Vector2.zero, closet))
                {
                    closest = Vector2.Distance(Vector2.zero, v);
                    closet = v;
                }
            }
            afterSortOrder.Add(closet);
            sortOrder.Remove(closet);
        }
        sortOrder = afterSortOrder;
    }
    float tempSpeed;
    public bool wandering = false;
    public string currentPriority = "";
    public float refreshTime = 1;
    public float currentTime = 1;
    Vector2 pastTarget;
    List<string> priorities;
    public Vector3 pos;
    public List<Vector2> targets = new List<Vector2>();
    IEnumerator UpdateAI()
    {
        
        yield return new WaitForSeconds(Random.Range(0.0f,3.0f));
        while (true)
        {
            yield return new WaitForSeconds(0);
            currentTime += Time.deltaTime;
            if (needsOrders)
            {
                if (currentTime >= refreshTime)
                {
                    currentTime = 0;
                    priorities = me.makePriorityList(stats, inCombat);
                    currentPriority = priorities[0];
                }

                    wandering = false;
                    if (priorities[0] == "needFood")
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

            if (getFood)
            {
                if (firstLoop)
                {
                    firstLoop = false;
                    targets = new List<Vector2>();
                    foreach (Vector2 v in sortOrder)
                    {
                        TerrainTileValues t;
                        if (GenerateGrid.foodList.TryGetValue(new GenerateGrid.coords((int)(v.x + pos.x), (int)(v.y + pos.y)), out t))
                        {
                            if (Vector2.Distance(transform.position, new Vector2(v.x + pos.x, v.y + pos.y)) > me.sight)
                            {
                                target = Vector2.zero;
                                break;
                            }
                            if (t.food)
                            {
                                target = new Vector2(v.x + pos.x, v.y + pos.y);
                                
                                if(target.x > 1 && target.y > 1)
                                {
                                    pastTarget = target;
                                    break;
                                }
                                
                                
                            }
                        }
                        target = Vector2.zero;
                    }                
                }      
            else
            {
                if (target != Vector2.zero && target.x > 1 && target.y > 1) {
                    wandering = false;
                    transform.position = Vector2.MoveTowards(pos, target, Time.deltaTime * tempSpeed);
                    if (Vector2.Distance(pos, target) < 0.01)
                    {
                        tempWait += Time.deltaTime;
                        eatingSymbol.SetActive(true);
                        if (tempWait > waitTime)
                        {
                            eatingSymbol.SetActive(false);
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
                else
                {
                    getFood = false;
                    firstLoop = true;
                    needsOrders = true;
                    wandering = true;
                }
            }
            }
            else if (getWater)
            {
                if (firstLoop)
                {
                    firstLoop = false;
                    targets = new List<Vector2>();
                    foreach (Vector2 v in sortOrder)
                    {
                        TerrainTileValues t;
                            if (GenerateGrid.grid.TryGetValue((new GenerateGrid.coords((int)(v.x + pos.x), (int)(v.y + pos.y))), out t))
                            {
                            if (Vector2.Distance(transform.position, new Vector2(v.x + pos.x, v.y + pos.y)) > me.sight)
                            {
                                target = Vector2.zero;
                                break;
                            }
                            if (t.code == 1111)
                                {
                                    target = new Vector2(v.x, v.y);
                                break;
                                }
                            }
                        target = Vector2.zero;
                    }
                 
                }
                else
                {
                    if (target != Vector2.zero)
                    {
                        wandering = false;
                        transform.position = Vector2.MoveTowards(pos, target, Time.deltaTime * tempSpeed);
                        if (Vector2.Distance(pos, target) < 0.01)
                        {
                            getWater = false;
                            firstLoop = true;
                            needsOrders = true;

                        }
                    }
                    else
                    {
                        wandering = true;
                        getFood = false;
                        firstLoop = true;
                        needsOrders = true;
                    }
                }
            }
            else if (getRest)
            {
                wandering = false;
                zzz.SetActive(true);
                tempWait += Time.deltaTime;
                if (tempWait > 5)
                {
                    zzz.SetActive(false);
                    getRest = false;
                    tempWait = 0;
                    needsOrders = true;
                }

            }
            else if (tryBreed)
            {
                wandering = false;
                if (firstLoop)
                {
                    firstLoop = false;
                    foreach (Brain b in CreateAIS.aiList)
                    {
                        if (b.wantBreed && Vector2.Distance(pos, b.pos) < me.sight)
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
                    if (breedTarget == null)
                    {
                        firstLoop = true;
                        tryBreed = false;
                        needsOrders = true;
                        wandering = true;
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(pos, breedTarget.pos, Time.deltaTime * tempSpeed);
                        if (Vector2.Distance(pos, breedTarget.pos) < 1)
                        {
                          
                            firstLoop = true;
                            tryBreed = false;
                            breedTarget = null;
                            needsOrders = true;
                            if (me.gender == 1)
                            {
                                BirthCountdown();
                            }
                        }

                    }
                }

            }
           
        }
    }
    float  redirTicks = 0;
    float currentTicks = 0;
    int wanderDir;
    // Update is called once per frame
    void Update () {
        pos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        TerrainTileValues tr;
        if (GenerateGrid.grid.TryGetValue(new GenerateGrid.coords((int)pos.x, (int)pos.y), out tr))
        {
            tempSpeed = me.speed * tr.speed;
        }
        else
        {
            tempSpeed = me.speed;
        }
        if(wandering)
        {
            needsOrders = true;
            Wander();
            currentTicks += Time.deltaTime;
            if(currentTicks >= redirTicks)
            {
                redirTicks = Random.Range(0.0f, 2.0f);
                currentTicks = 0;
                wanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
    }
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
    }
	public void BirthCountdown () {
		r.isPregnant = true;
		r.energyIncrease = 0.75f;
		Invoke ("HaveKid", 60);
	}
	public void HaveKid() {
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
