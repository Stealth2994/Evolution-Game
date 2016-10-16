using UnityEngine;
using System.Collections;
[System.Serializable]
public class ItemStack {
    public ItemStack(string name,Item item, int amount )
    {
        this.name = name;
        this.amount = amount;
        this.item = item;
    }
    public string name;
    public int amount;
    public Item item;
}
