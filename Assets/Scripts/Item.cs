using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{

    // Deprecated 

    //public enum ItemType
    //{
    //    Table,
    //    Chair,
    //    Decoration,
    //    WallDecoration
    //}

    //public ItemType itemType;
    public string name;
    public int amount;

    public Texture2D GetTexture2DByName()
    {
        return ItemAssets.Instance.itemPool[name];
    }
}
