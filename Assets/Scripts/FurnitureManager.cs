using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FurnitureManager : MonoBehaviour
{

    private GameObject furniture;

    public PlacementManager placementManager;

    public List<Vector3Int> tempPlacementPos = new List<Vector3Int>();
    //private Dictionary<Vector3Int, CellType> FurnitureDic = new Dictionary<Vector3Int, CellType>();

    [SerializeField]
    GameObject table, chair;

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
    }

    public void PlaceFurniture(Vector3Int pos)
    {
        if (placementManager.CheckIfPositionInBound(pos) == false) 
            return;
        if (placementManager.CheckIfPositionIsFree(pos) == false)
            return;

        placementManager.PlaceTemporaryStructure(pos, furniture, CellType.Furniture);

        // If succsessfully placed a furniture, store that into dictionary
        //FurnitureDic.Add(new Vector3Int(pos.x, pos.z, ), );

    }
}
