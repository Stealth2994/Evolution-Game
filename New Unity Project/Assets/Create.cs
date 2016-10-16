using UnityEngine;
using System.Collections;

public class Create : MonoBehaviour {
    public Recipe r;
    public void CreateItem()
    {
        Inventory i = GameObject.Find("Inventory").GetComponent<Inventory>();
        i.AddItem(r.outcome, r.outcome.item.code);
        foreach(ItemStack it in r.items)
        {
            i.RemoveItem(it.item.code, it.amount);
        }
       
    }
}
