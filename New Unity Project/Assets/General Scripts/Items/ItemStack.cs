using UnityEngine;
using System.Collections;
[System.Serializable]
public class ItemStack {
    public ItemStack(Sprite picture, string name,Item item, int amount )
    {
        this.picture = picture;
        this.name = name;
        this.amount = amount;
        this.item = item;
    }
    public Sprite picture;
    public string name;
    public int amount;
    public Item item;
}
