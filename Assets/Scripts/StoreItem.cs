using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoreItem
{
    public string name;
    public int price;

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
