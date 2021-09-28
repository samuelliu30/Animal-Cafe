using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
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

    public SerializableDictionary<string, GameObject> wallPool;

    public void ChangePlacement(string name)
    {
        decorator = wallPool[name];
        placementManager.ShowTemporalObject(decorator, CellType.WallDecorator);
    }

    internal void PlaceDecorator(Vector3Int pos)
    {
        if (placementManager.CheckIfPositionInBound(pos) == false)
            return;
        if (placementManager.CheckIfPositionIsFree(pos) == false)
            return;

        placementManager.PlaceTemporaryStructure(pos, decorator, CellType.WallDecorator);
        //if (fromInventory)
        //{
        //    bagManager.RemoveItem(furniture);
        //}
        //fromInventory = false;
        decorator = null;
    }
}
