using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Table,
        Chair,
    }

    public ItemType itemType;
    public string name;
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

    public Texture2D GetTexture2DByName()
    {
        switch (name)
        {
            default:
            case "table": return ItemAssets.Instance.table;
            case "chair": return ItemAssets.Instance.chair;
        }
    }
}
