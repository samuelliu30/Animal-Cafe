using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagManager
{

    private Dictionary<string, Item> itemDict;

    public BagManager()
    {
        itemDict = new Dictionary<string, Item>();
    }

    public BagManager(Dictionary<string, Item> itemDict)
    {
        this.itemDict = itemDict;
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
        //itemList.Add(item);
    }

}
