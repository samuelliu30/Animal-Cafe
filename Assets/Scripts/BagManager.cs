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

        tableDict.Add("table01Wooden", new Item { itemType = Item.ItemType.Table, amount = 10, name = "table01Wooden" });
        tableDict.Add("table02Wooden", new Item { itemType = Item.ItemType.Table, amount = 10, name = "table02Wooden" });
        chairDict.Add("chairBrown", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chairBrown" });
        chairDict.Add("chairBlue", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chairBlue" });
        chairDict.Add("chairBlack", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chairBlack" });
        chairDict.Add("chairGrey", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chairGrey" });
        chairDict.Add("chairWhite", new Item { itemType = Item.ItemType.Chair, amount = 10, name = "chairWhite" });
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
        if (name.Contains("chair"))
        {
            RemoveFromCategory(chairDict, name);
        }
        else
        {
            RemoveFromCategory(tableDict, name);
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
