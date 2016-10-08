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
    bool getWater;
    bool getRest;
    bool tryBreed;
    bool wander;
	// Use this for initialization
	void Start () {
        me = GetComponent<Genes>();
        stats = GetComponent<SurvivalStats>();
        me.CreateGenes(mom, dad);
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
