using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagManager
{
    public event EventHandler OnItemListChanged;
    private Dictionary<string, Item> itemDict;

    public BagManager()
    {
        itemDict = new Dictionary<string, Item>();
    }

    public Dictionary<string, Item> ItemDict
    {
        get
        {
            return itemDict;
        }
    }

    public void AddItem(Item item)
    {
        if (itemDict.ContainsKey(item.name))
        {
            itemDict[item.name].amount += 1;
        }
        else
        {
            itemDict.Add(item.name, item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        //itemList.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (itemDict[item.name].amount == 1)
        {
            itemDict.Remove(item.name);
        }
        else
        {
            itemDict[item.name].amount -= 1;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateItemList(Dictionary<string, Item> itemDict)
    {
        this.itemDict = itemDict;
    }

}
