using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class Crafter : MonoBehaviour {
    public List<Recipe> craftables = new List<Recipe>();
    public Button CraftButton;
    public Transform ButtonGrid;
    public Transform location;
    // Use this for initialization
    void Start()
    {
        location = GameObject.Find("Canvas").transform.FindChild("InventoryPanel").FindChild("Create Panel").transform;
    }
    void UpdateInventory()
    {
        foreach (Transform t in location)
        {
            Destroy(t.gameObject);
        }
        foreach (Recipe entry in craftables)
        {
            Button g = Instantiate(CraftButton);
            g.transform.SetParent(location);
            g.transform.FindChild("Name").GetComponent<Text>().text = entry.Outcome.name;
            g.transform.FindChild("Image").GetComponent<Image>().sprite = entry.Outcome.picture;
            g.GetComponent<Create>().r = entry;
            string recipe = "";
           foreach(ItemStack i in entry.items)
            {
                recipe += i.name + " + ";
            }
            g.transform.FindChild("Recipe").GetComponent<Text>().text = recipe;
        }
    }


    public void Reset()
    {
        craftables = new List<Recipe>();
    }
    public void AddItem(Recipe newItem)
    {
        if (craftables.Contains(newItem))
        {
            return;
        }
        else
        {
            
            craftables.Add(newItem);
            UpdateInventory();
        }

    }
    public bool RemoveItem(Recipe recipe)
    {
        craftables.Remove(recipe);
        UpdateInventory();
        return true;
    }
}
