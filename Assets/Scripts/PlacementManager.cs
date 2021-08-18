using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid diningRoomGrid;

    private GameObject tempStructure;
    private GameObject placementIndicator;

    private void Start()
    {
        diningRoomGrid = new Grid(width, height);
    }

    private void Update()
    {
        // If player is trying to place an object, move the preview with mouse
        if (tempStructure)
        {
            // Convert Screen unit to game world unit
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                tempStructure.transform.position = new Vector3(hit.point.x, 1, hit.point.z);

                //TODO: Optimize the indicator movement. The update is lagged right now

                // Cast the indicator to the top surface
                float surface = hit.collider.ClosestPointOnBounds(hit.point).y;
                placementIndicator.transform.position = new Vector3(hit.point.x, surface, hit.point.z);

                // Convert Vector3 to Vector3Int
                Vector3Int pointInt = new Vector3Int(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y), Mathf.FloorToInt(hit.point.z));
                if(CheckIfPositionInBound(pointInt))
                {
                    ChangeIndicatorColor(pointInt);
                }
            }

            if (Input.GetKeyDown((KeyCode)'r')) {
                tempStructure.transform.Rotate(Vector3.up, 90.0f);
                placementIndicator.transform.Rotate(Vector3.up, 90.0f);
            }
        }
    }

    private void ChangeIndicatorColor(Vector3Int pos)
    {
        var render = placementIndicator.GetComponent<Renderer>();

        if (CheckIfPositionIsFree(pos))
        {
            render.material.SetColor("_Color", Color.green);
        }
        else
        {
            render.material.SetColor("_Color", Color.red);
        }
    }

    internal bool CheckIfPositionInBound(Vector3Int pos)
    {
        if((pos.x > 0)&&(pos.x < width)&&(pos.z > 0)&&(pos.z < height))
            return true;
        else
            return false;
    }

    internal bool CheckIfPositionIsFree(Vector3Int pos)
    {
        return diningRoomGrid.GetStatusOfCell(pos.x, pos.z) == 0;
    }

    internal void PlaceTemporaryStructure(Vector3Int pos, GameObject item, CellType cellType)
    {
        GameObject newStructure = Instantiate(item, pos, tempStructure.transform.rotation);
        diningRoomGrid.SetGridOccupied(pos.x, pos.z);

        // Destroy Indicator
        GameObject.Destroy(tempStructure);
        GameObject.Destroy(placementIndicator);
    }

    // Display a preview of the object player trying to place
    public void ShowTemporalObject(GameObject item, GameObject preview)
    {
        tempStructure = Instantiate(item, Input.mousePosition, Quaternion.identity);
        //Vector3 size = tempStructure.GetComponent<Renderer>().bounds.size;
        placementIndicator = Instantiate(preview, tempStructure.transform.position, Quaternion.identity);
    }
}
