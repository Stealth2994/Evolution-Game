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
	    foreach(Recipe r in recipes)
        {
            foreach(ItemStack i in r.items)
            {
                if(pi.inventory.ContainsKey(i.item.code))
                {
                    if(pi.inventory[i.item.code].Amount >= i.Amount)
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
            GetComponent<Crafter>().AddItem(r);
            continue;
            getOut:;
            GetComponent<Crafter>().RemoveItem(r);
        }
	}
  
}
