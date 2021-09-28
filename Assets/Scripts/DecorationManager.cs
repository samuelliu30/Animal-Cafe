using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationManager : MonoBehaviour
{

    // Deprecated class

    private GameObject decorator;
    public PlacementManager placementManager;
    public List<Vector3Int> tempPlacementPos = new List<Vector3Int>();
    public class DecoratorData
    {
        public string name;
        public Quaternion rotation;
    }


    private Dictionary<Vector3Int, DecoratorData> postionFurnitureDic = new Dictionary<Vector3Int, DecoratorData>();

    public SerializableDictionary<string, GameObject> DecoratorPool;

    public void ChangePlacement(string name)
    {
        decorator = DecoratorPool[name];
        placementManager.ShowTemporalObject(decorator, CellType.Decorator);
    }

    internal void PlaceDecorator(Vector3Int pos)
    {
        if (placementManager.CheckIfPositionInBound(pos) == false)
            return;
        if (placementManager.CheckIfPositionIsFree(pos) == false)
            return;

        placementManager.PlaceTemporaryStructure(pos, decorator, CellType.Decorator);
        //if (fromInventory)
        //{
        //    bagManager.RemoveItem(furniture);
        //}
        //fromInventory = false;
        decorator = null;
    }
}
