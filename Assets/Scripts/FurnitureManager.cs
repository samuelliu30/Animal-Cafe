using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class FurnitureManager : MonoBehaviour
{

    private string furniture;
    private GameObject tempStructure;

    public PlacementManager placementManager;
    public ItemAssets itemAssets;

    private BagManager bagManager;
    public bool fromInventory = false;

    public List<Vector3Int> tempPlacementPos = new List<Vector3Int>();
    public class FurnitureData
    {
        // A class to store furniture specs

        public string name;
        public Quaternion rotation;
    }

    public string Furniture
    {
        get
        {
            return furniture;

        }
    }


    private Dictionary<Vector3Int, FurnitureData> postionFurnitureDic = new Dictionary<Vector3Int, FurnitureData>();
    private Dictionary<Vector3Int, FurnitureData> positionDecorationDic = new Dictionary<Vector3Int, FurnitureData>();

    public SerializableDictionary<string, GameObject> furniturePool;

    public void SetBagManager(BagManager bagManager)
    {
        this.bagManager = bagManager;
    }


    public void ChangePlacement(string name, string type = "table")
    {

        furniture = name;
        //placementManager.ShowTemporalObject(furniturePool[furniture], CellType.Furniture);

        switch (type)
        {
            case "table":
                placementManager.ShowTemporalObject(itemAssets.tablePool[furniture], CellType.Furniture);
                break;
            case "chair":
                placementManager.ShowTemporalObject(itemAssets.chairPool[furniture], CellType.Furniture);
                break;
            case "decoration":
                placementManager.ShowTemporalObject(itemAssets.decoPool[furniture], CellType.Decorator);
                break;
            case "wall":
                placementManager.ShowTemporalObject(itemAssets.wallPool[furniture], CellType.WallDecorator);
                break;
            default:
                break;
        }

    }

    public void ChangePlacement(string name, float rotation)
    {
        //TODO: Change the furniture name
        name = name.Split('(')[0];
        furniture = name;

        placementManager.ShowTemporalObject(furniturePool[furniture], CellType.Furniture, rotation);
    }

    public void PlaceFurniture()
    {
        if (fromInventory)
        {
            bagManager.RemoveItem(furniture);
        }
        fromInventory = false;
        furniture = null;

    }
    public Dictionary<Vector3Int, FurnitureData> PositionFurnitureDic
    {
        get
        {
            return postionFurnitureDic;
        }
    }


    public void PickUp()
    {
        // Convert Screen unit to game world unit
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                GameObject c = hit.collider.gameObject;
                if (c.tag == "Furniture" || c.tag == "AvailableChair")
                {
                    placementManager.FreePosition(Vector3Int.RoundToInt(c.transform.position));
                    postionFurnitureDic.Remove(Vector3Int.RoundToInt(c.transform.position));
                    ChangePlacement(c.name, c.transform.rotation.eulerAngles.y);
                    GameObject.Destroy(c);
                }

            }

        }
    }

    public void Store(GameObject c)
    {
        placementManager.FreePosition(Vector3Int.CeilToInt(c.transform.position));
        postionFurnitureDic.Remove(Vector3Int.CeilToInt(c.transform.position));
        GameObject.Destroy(c);
    }

#nullable enable
    internal GameObject? GetItem(string name)
    {
        if (furniturePool.ContainsKey(name))
        {
            return furniturePool[name];
        }
        else
        {
            return null;
        }
    }

    public bool IfPlaceable()
    {
        return furniture != null;
    }

    public void WritePositionDict(Vector3Int pos, string name, Quaternion rotation)
    {
        FurnitureData tmp = new FurnitureData();
        tmp.name = furniture;
        tmp.rotation = rotation;

        // Add new furniture to the position Dictionary
        postionFurnitureDic[pos] = tmp;
    }
}
