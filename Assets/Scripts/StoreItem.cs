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
        return ItemAssets.Instance.itemPool[name];
    }

}
