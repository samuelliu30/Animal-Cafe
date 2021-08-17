using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid diningRoomGrid;
    private void Start()
    {
        diningRoomGrid = new Grid(width, height);
    }

    internal bool CheckIfPositionInBound(Vector3Int pos)
    {
        if((pos.x >= 0)&&(pos.x < width)&&(pos.z >= 0)&&(pos.z <= height))
            return true;
        else
            return false;
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
