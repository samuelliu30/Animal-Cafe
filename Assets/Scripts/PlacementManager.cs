using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal bool CheckIfPositionInBound(Vector3Int pos)
    {
        return true;
    }

    internal bool CheckIfPositionIsFree(Vector3Int pos)
    {
        return true;
    }

    internal void PlaceTemporaryStructure(Vector3Int pos, GameObject item, CellType cellType)
    {
        GameObject newStructure = Instantiate(item, pos, Quaternion.identity);
    }
}
