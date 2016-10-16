using UnityEngine;
using System.Collections.Generic;

public class Craft : MonoBehaviour {
    public List<Recipe> recipes = new List<Recipe>();
    private Inventory pi;
	// Use this for initialization
	void Start () {
        pi = GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	public void invChanged () {
        Debug.Log("ive been called");
	    foreach(Recipe r in recipes)
        {
            foreach(ItemStack i in r.items)
            {
                if(pi.inventory.ContainsKey(i.item.code))
                {
                    if(pi.inventory[i.item.code].amount >= i.amount)
                    {
                        Debug.Log("well here");
                    }
                    else
                    {
                        goto getOut;
                    }
                }
                else
                {
                    goto getOut;
                }
            }
            Debug.Log("YAA BOIZ");
            GetComponent<Crafter>().AddItem(r);
            continue;
            getOut:;
            Debug.Log("DELETED U NERDS");

            GetComponent<Crafter>().RemoveItem(r);
        }
	}
  
}
