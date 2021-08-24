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
        itemDict.Add("table", new Item { itemType = Item.ItemType.Table, amount = 10, name = "table"});
        itemDict.Add("chair", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chair"});
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

    public void RemoveItem(string name)
    {
        if (itemDict[name].amount == 1)
        {
            itemDict.Remove(name);
        }
        else
        {
            itemDict[name].amount -= 1;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateItemList(Dictionary<string, Item> itemDict)
    {
        this.itemDict = itemDict;
    }

}
