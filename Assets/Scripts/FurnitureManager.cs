using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FurnitureManager : MonoBehaviour
{

    private GameObject furniture;
    private GameObject preview;

    public PlacementManager placementManager;

    public List<Vector3Int> tempPlacementPos = new List<Vector3Int>();
    //private Dictionary<Vector3Int, CellType> FurnitureDic = new Dictionary<Vector3Int, CellType>();
    private Dictionary<CellType, List<Vector3Int>> furniturePositionDic = new Dictionary<CellType, List<Vector3Int>>();

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

        furniture = furniturePool[name];

        // Initialize the placement indicator
        Vector3 furnitureSize = furniture.GetComponent<Renderer>().bounds.size / 2;

        // TODO: Dynamically initialize the mesh size of the indicator
        //       Currently the size is hardcoded and scale by the factor of funiture size
        preview.transform.localScale = new Vector3(0.25f * furnitureSize.x, 0.1f, 0.25f * furnitureSize.z);
        placementManager.ShowTemporalObject(furniture, preview);
    }

    public void ChangePlacement(string name, float rotation)
    {
        //TODO: Change the furniture name

        // Switch placement furniture type
        switch (name)
        {
            case "Restaurant Table 01 Wooden(Clone)":
                furniture = table;
                break;
            case "Restaurant Chair 01 Brown(Clone)":
                furniture = chair;
                break;
            default:
                break;
        }

        // Initialize the placement indicator
        Vector3 furnitureSize = furniture.GetComponent<Renderer>().bounds.size / 2;

        // TODO: Dynamically initialize the mesh size of the indicator
        //       Currently the size is hardcoded and scale by the factor of funiture size
        preview.transform.localScale = new Vector3(0.25f * furnitureSize.x, 0.1f, 0.25f * furnitureSize.z);
        placementManager.ShowTemporalObject(furniture, preview, rotation);
    }

    public void PlaceFurniture(Vector3Int pos)
    {

        if (placementManager.CheckIfPositionInBound(pos) == false) 
            return;
        if (placementManager.CheckIfPositionIsFree(pos) == false)
            return;

        placementManager.PlaceTemporaryStructure(pos, furniture, CellType.Furniture);
        if (furniturePositionDic.ContainsKey(CellType.Furniture))
        {
            furniturePositionDic[CellType.Furniture].Add(pos);
        }
        else 
        {
            List<Vector3Int> tmp = new List<Vector3Int>();
            tmp.Add(pos);
            furniturePositionDic[CellType.Furniture] = tmp;
        }

        furniture = null;
        // If succsessfully placed a furniture, store that into dictionary
        //FurnitureDic.Add(new Vector3Int(pos.x, pos.z, ), );

    }
    public Dictionary<CellType, List<Vector3Int>> FurniturePositionDic
    {
        get
        {
            return furniturePositionDic;
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
                    ChangePlacement(c.name, c.transform.rotation.eulerAngles.y);
                    GameObject.Destroy(c);
                }

            }

        }

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
}
