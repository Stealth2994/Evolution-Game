using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    public Dictionary<int, ItemStack> inventory = new Dictionary<int, ItemStack>();
    public Button NewButton;
    public Transform ButtonGrid;
    public Transform location;
    // Use this for initialization
    void Start () {
        location = GameObject.Find("Canvas").transform.FindChild("InventoryPanel").FindChild("Panel").transform;
    }
    void UpdateInventory()
    {
        GetComponent<Craft>().invChanged();
       foreach(Transform t in location)
        {
            Destroy(t.gameObject);
        }
       foreach(KeyValuePair<int,ItemStack> entry in inventory)
        {
            Button g = Instantiate(NewButton);
            g.transform.SetParent(location);
            g.transform.FindChild("Text").GetComponent<Text>().text = entry.Value.amount + " " + entry.Value.name;
        }
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
            UpdateInventory();
        }
        else
        {
            inventory.Add(code, newItem);
            UpdateInventory();
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
                UpdateInventory();
                return true;
            }
            else if(alreadyInv.amount == amount)
            {
                inventory.Remove(code);
                UpdateInventory();
                return true;
            }
            else
            {
                UpdateInventory();
                return false;
            }
          
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
