using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Recipe {
    public List<ItemStack> items;
    [SerializeField]
    private ItemStack outcome;
    public ItemStack Outcome
    {
        get { return outcome; }
        set { Debug.Log("ive been set");
           outcome = value; }
    }

}
