using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    public SerializableDictionary<string, Texture2D> itemPool;

    public SerializableDictionary<string, GameObject> tablePool;
    public SerializableDictionary<string, GameObject> chairPool;
    public SerializableDictionary<string, GameObject> decoPool;
    public SerializableDictionary<string, GameObject> wallPool;

    public SerializableDictionary<string, int> priceDict;

    private void Awake()        
    {
        Instance = this;
    }

}
