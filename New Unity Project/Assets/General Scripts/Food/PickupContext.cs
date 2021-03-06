﻿using UnityEngine;
using System.Collections;

public class PickupContext : ContextBase {
    public void OnMouseDown()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Player.transform.position.x - (transform.position.x + 1), 2) + Mathf.Pow(Player.transform.position.y - (transform.position.y - 2), 2));
        if (distance < range)
        {
            GenerateGrid.removeFoodList.Add(new GenerateGrid.coords((int)transform.parent.parent.position.x, (int)transform.parent.parent.position.y), transform.parent.parent.gameObject);
            GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(new ItemStack(transform.parent.parent.GetComponent<Item>().image, transform.parent.parent.GetComponent<UsableValues>().name, transform.parent.parent.GetComponent<Item>(), transform.parent.parent.GetComponent<UsableValues>().amount),transform.parent.parent.GetComponent<Item>().code);
            
        }
        ContextSprite.SetActive(false);
    }

}
