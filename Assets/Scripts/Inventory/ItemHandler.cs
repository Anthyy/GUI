﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public int itemId = 0;
    public ItemTypes itemType;
    public int amount;
    public void OnCollection()
    {
        if (itemType == ItemTypes.Money)
        {
            Inventory.money += amount; // add to money
        }
        else if (itemType == ItemTypes.Craftable || itemType == ItemTypes.Consumables)
        {
            int found = 0;
            int addIndex = 0;
            for(int i = 0; i < Inventory.inv.Count; i++)
            {
                if(itemId == Inventory.inv[i].Id)
                {
                    found = 1;
                    addIndex = i;
                    break;
                }
            }
            if(found == 1)
            {
                Inventory.inv[addIndex].Amount += amount;
            }
            else
            {
                Inventory.inv.Add(ItemData.CreateItem(itemId)); // pick up and add to inventory
                if(amount > 1)
                {
                    for (int i = 0; i < Inventory.inv.Count; i++)
                    {
                        if (itemId == Inventory.inv[i].Id)
                        {
                            Inventory.inv[i].Amount = amount;
                        }
                    }
                }               
            }
        }
        else // Weapons or Armour/Misc
        {
            Inventory.inv.Add(ItemData.CreateItem(itemId)); // pick up and add to inventory
        }
        Destroy(gameObject); // Remove the object this script is attached to from the world
    }
}
