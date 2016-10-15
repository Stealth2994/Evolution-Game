using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public Dictionary<int, ItemStack> inventory = new Dictionary<int, ItemStack>();
	// Use this for initialization
	void Start () {
	
	}
	public void Reset()
    {
        inventory = new Dictionary<int, ItemStack>();
    }
    public void AddItem(ItemStack newItem, int code)
    {
        ItemStack alreadyInv;
        if(inventory.TryGetValue(code, out alreadyInv))
        {
            alreadyInv.amount += newItem.amount;
        }
        else
        {
            inventory.Add(code, newItem);
        }

    }
    public bool RemoveItem(int code, int amount)
    {
        ItemStack alreadyInv;
        if (inventory.TryGetValue(code, out alreadyInv))
        {
            if(alreadyInv.amount > amount)
            {
                alreadyInv.amount -= amount;
                return true;
            }
            else if(alreadyInv.amount == amount)
            {
                inventory.Remove(code);
                return true;
            }
            else
            {
                return false;
            }
          
            return true;
        }
        return false;
    }
    public int GetStackSize(int code)
    {
        ItemStack alreadyInv;
        if (inventory.TryGetValue(code, out alreadyInv))
        {
            return alreadyInv.amount;
        }
        return 0;
    }
}
