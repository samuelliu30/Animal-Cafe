using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagManager
{
    public event EventHandler OnItemListChanged;
    private Dictionary<string, Item> itemDict;
    private Dictionary<string, Item> tableDict;
    private Dictionary<string, Item> chairDict;

    public BagManager()
    {
        itemDict = new Dictionary<string, Item>();
        tableDict = new Dictionary<string, Item>();
        chairDict = new Dictionary<string, Item>();
        itemDict.Add("table", new Item { itemType = Item.ItemType.Table, amount = 10, name = "table"});
        itemDict.Add("chair", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chair"});
        tableDict.Add("table", new Item { itemType = Item.ItemType.Table, amount = 10, name = "table" });
        tableDict.Add("tableWhite", new Item { itemType = Item.ItemType.Table, amount = 10, name = "tableWhite" });
        chairDict.Add("chair", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chair" });
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
    public Dictionary<string, Item> ChairDict
    {
        get
        {
            return chairDict;
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
        switch (name)
        {
            case "table":
                RemoveFromCategory(tableDict, name);
                break;
            case "chair":
                RemoveFromCategory(chairDict, name);
                break;
            default:
                break;
        }
    }

    public void RemoveFromCategory(Dictionary<string, Item> dict, string name)
    {
        if (dict[name].amount == 1)
        {
            dict.Remove(name);
        }
        else
        {
            dict[name].amount -= 1;
        }
    }

    public void UpdateItemList(Dictionary<string, Item> itemDict)
    {
        this.itemDict = itemDict;
    }

}
