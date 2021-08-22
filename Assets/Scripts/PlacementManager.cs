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
    public FurnitureManager furnitureManager;
    private bool move = false;
    private FurnitureController furnitureController;

    public Material indicator;
    public Material resturantMaterial;

    private void Start()
    {
        diningRoomGrid = new Grid(width, height);
        move = false;
    }

    private void Update()
    {
        // If player is trying to place an object, move the preview with mouse
        if (tempStructure)
        {
            //Debug.Log(furnitureController.ifPlaceable);
            // Convert Screen unit to game world unit
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                tempStructure.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                if (!furnitureController.IfPlaceable)
                {
                    tempStructure.GetComponent<Renderer>().material = indicator;
                }
                else
                {
                    tempStructure.GetComponent<Renderer>().material = resturantMaterial;
                }
            }

            if (Input.GetKeyDown((KeyCode)'r')) {
                tempStructure.transform.Rotate(Vector3.up, 90.0f);
            }
        }
    }

    internal bool CheckIfPositionInBound(Vector3Int pos)
    {
        Debug.Log("in check bound");
        if((pos.x > 0)&&(pos.x < width)&&(pos.z > 0)&&(pos.z < height)&&(pos.y == 0))
            return true;
        else
            return false;
    }

    internal bool CheckIfPositionIsFree(Vector3Int pos)
    {
        bool cuck = diningRoomGrid.GetStatusOfCell(pos.x, pos.z) == 0;
        Debug.Log(cuck);
        return cuck;
    }

    internal void PlaceTemporaryStructure(Vector3Int pos, GameObject item, CellType cellType)
    {
        GameObject newStructure = Instantiate(item, pos, tempStructure.transform.rotation);
        if(cellType == CellType.Furniture)
        {
            furnitureManager.WritePositionDict(pos, furnitureManager.Furniture, tempStructure.transform.rotation);
        }
        diningRoomGrid.SetGridStatus(pos.x, pos.z, 1);

        // Destroy Indicator
        GameObject.Destroy(tempStructure);
        GameObject.Destroy(placementIndicator);
    }

    internal void FreePosition(Vector3Int pos)
    {
        diningRoomGrid.SetGridStatus(pos.x, pos.z, 0);
    }

    // Display a preview of the object player trying to place
    public void ShowTemporalObject(GameObject item, float rotation = 0)
    {
        Vector3 pos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
        tempStructure = Instantiate(item, pos, Quaternion.Euler(0, rotation, 0));
        furnitureController = tempStructure.GetComponent<FurnitureController>();
    }

    internal void PlaceObject(Vector3Int pos, string name, Quaternion rotation, CellType type)
    {
        if (type == CellType.Furniture)
        {
            GameObject item = furnitureManager.GetItem(name);
            if (item != null)
            {
                GameObject tmp = Instantiate(item, pos, rotation);
            }
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public void CancelCurrentMovement()
    {
        if(tempStructure != null)
        {
            Destroy(tempStructure.gameObject);
        }
        if(placementIndicator != null)
        {
            Destroy(placementIndicator.gameObject);
        }
    }

    internal bool IfMove()
    {
        return move;
    }
    public void Move()
    {
        move = !move;
    }
}
