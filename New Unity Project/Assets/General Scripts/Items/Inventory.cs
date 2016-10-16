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
            g.transform.FindChild("Text").GetComponent<Text>().text = entry.Value.Amount + " " + entry.Value.name;
        }
    }
      
    
	public void Reset()
    {
        inventory = new Dictionary<int, ItemStack>();
    }
    public void AddItem(ItemStack newItem, int code)
    {
        Debug.Log(newItem.Amount);
        ItemStack alreadyInv;
        if(inventory.TryGetValue(code, out alreadyInv))
        {
            alreadyInv.Amount += newItem.Amount;
            UpdateInventory();
        }
        else
        {
            inventory.Add(code, new ItemStack(newItem.picture,newItem.name,newItem.item,newItem.Amount));
            UpdateInventory();
        }
      
    }
    public bool RemoveItem(int code, int amount)
    {
        ItemStack alreadyInv;
        if (inventory.TryGetValue(code, out alreadyInv))
        {
            if(alreadyInv.Amount > amount)
            {
                alreadyInv.Amount -= amount;
                UpdateInventory();
                return true;
            }
            else if(alreadyInv.Amount == amount)
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
            return alreadyInv.Amount;
        }
        return 0;
    }
}
