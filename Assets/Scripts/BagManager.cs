using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagManager
{
    private List<Item> itemList;

    public BagManager()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Chair, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Table, amount = 1 });
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
