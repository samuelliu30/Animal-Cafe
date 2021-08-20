using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FurnitureManager : MonoBehaviour
{

    private string furniture;
    private GameObject preview;

    public PlacementManager placementManager;
    public ItemWorld itemWorld;
    public BagManager bagManager;

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

    [SerializeField]
    GameObject table, chair, indicator;

    private Dictionary<string, GameObject> furniturePool = new Dictionary<string, GameObject>();

    private void Start()
    {
        preview = indicator;
        furniturePool["table"] = table;
        furniturePool["chair"] = chair;
    }

    public void ChangePlacement(string name)
    {

        furniture = name;

        // Initialize the placement indicator
        Vector3 furnitureSize = furniturePool[name].GetComponent<Renderer>().bounds.size / 2;

        // TODO: Dynamically initialize the mesh size of the indicator
        //       Currently the size is hardcoded and scale by the factor of funiture size
        preview.transform.localScale = new Vector3(0.25f * furnitureSize.x, 0.1f, 0.25f * furnitureSize.z);
        placementManager.ShowTemporalObject(furniturePool[name], preview);
    }

    public void ChangePlacement(string name, float rotation)
    {
        //TODO: Change the furniture name

        // Switch placement furniture type
        switch (name)
        {
            case "Restaurant Table 01 Wooden(Clone)":
                furniture = "table";
                break;
            case "Restaurant Chair 01 Brown(Clone)":
                furniture = "chair";
                break;
            default:
                break;
        }

        // Initialize the placement indicator
        Vector3 furnitureSize = furniturePool[furniture].GetComponent<Renderer>().bounds.size / 2;

        // TODO: Dynamically initialize the mesh size of the indicator
        //       Currently the size is hardcoded and scale by the factor of funiture size
        preview.transform.localScale = new Vector3(0.25f * furnitureSize.x, 0.1f, 0.25f * furnitureSize.z);
        placementManager.ShowTemporalObject(furniturePool[furniture], preview, rotation);
    }

    public void PlaceFurniture(Vector3Int pos)
    {

        if (placementManager.CheckIfPositionInBound(pos) == false) 
            return;
        if (placementManager.CheckIfPositionIsFree(pos) == false)
            return;

        placementManager.PlaceTemporaryStructure(pos, furniturePool[furniture], CellType.Furniture);

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
                if (c.tag == "Furniture")
                {
                    placementManager.FreePosition(Vector3Int.CeilToInt(c.transform.position));
                    postionFurnitureDic.Remove(Vector3Int.CeilToInt(c.transform.position));
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
