using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Table,
        Chair,
    }

    public ItemType itemType;
    public int amount;

    public Texture2D GetTexture2D()
    {
        switch (itemType)
        {
            default:
            case ItemType.Table:    return ItemAssets.Instance.table;
            case ItemType.Chair:    return ItemAssets.Instance.chair;
        }
    }
}
