using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FurnitureManager : MonoBehaviour
{

    private GameObject furniture;
    private GameObject preview;

    public PlacementManager placementManager;

    public List<Vector3Int> tempPlacementPos = new List<Vector3Int>();
    //private Dictionary<Vector3Int, CellType> FurnitureDic = new Dictionary<Vector3Int, CellType>();
    private Dictionary<CellType, List<Vector3Int>> FurniturePositionDic = new Dictionary<CellType, List<Vector3Int>>();

    [SerializeField]
    GameObject table, chair, indicator;

    private void Start()
    {
        preview = indicator;
    }

    public void changePlacement(int i)
    {
        // Switch placement furniture type
        Debug.Log(i);
        switch (i)
        {
            case 0:
                furniture = table;
                break;
            case 1:
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
        placementManager.ShowTemporalObject(furniture, preview);
    }

    public void PlaceFurniture(Vector3Int pos)
    {
        if (placementManager.CheckIfPositionInBound(pos) == false) 
            return;
        if (placementManager.CheckIfPositionIsFree(pos) == false)
            return;

        placementManager.PlaceTemporaryStructure(pos, furniture, CellType.Furniture);
        if (FurniturePositionDic.ContainsKey(CellType.Furniture))
        {
            FurniturePositionDic[CellType.Furniture].Add(pos);
        }
        else 
        {
            List<Vector3Int> tmp = new List<Vector3Int>();
            tmp.Add(pos);
            FurniturePositionDic[CellType.Furniture] = tmp;
        }

        // If succsessfully placed a furniture, store that into dictionary
        //FurnitureDic.Add(new Vector3Int(pos.x, pos.z, ), );

    }
}
