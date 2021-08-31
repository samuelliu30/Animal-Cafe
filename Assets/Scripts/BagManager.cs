using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagManager
{
    public event EventHandler OnItemListChanged;
    private Dictionary<string, Item> itemDict;
    private Dictionary<string, Item> tableDict;

    public BagManager()
    {
        itemDict = new Dictionary<string, Item>();
        tableDict = new Dictionary<string, Item>();
        itemDict.Add("table", new Item { itemType = Item.ItemType.Table, amount = 10, name = "table"});
        itemDict.Add("chair", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chair"});
        tableDict.Add("table", new Item { itemType = Item.ItemType.Table, amount = 10, name = "table" });
        tableDict.Add("tableWhite", new Item { itemType = Item.ItemType.Table, amount = 10, name = "tableWhite" });
    }

    public Dictionary<string, Item> ItemDict
    {
        get
        {
            return itemDict;
        }
    }
    public Dictionary<string, Item> TableDict
    {
        get
        {
            return tableDict;
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
