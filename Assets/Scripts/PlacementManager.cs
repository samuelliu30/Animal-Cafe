using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid diningRoomGrid;

    private GameObject tempStructure;
    public CellType cellType;

    public FurnitureManager furnitureManager;
    public bool move = false;
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
            if(cellType == CellType.Furniture)
            {
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

                if (Input.GetKeyDown((KeyCode)'r'))
                {
                    tempStructure.transform.Rotate(Vector3.up, 90.0f);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3Int pos = Vector3Int.RoundToInt(tempStructure.transform.position);
                    if((CheckIfPositionInBound(pos) == true)&& (CheckIfPositionIsFree(pos) == true))
                    {
                        PlaceTemporaryStructure(Vector3Int.RoundToInt(tempStructure.transform.position), tempStructure, cellType);
                        furnitureManager.PlaceFurniture();
                    }

                }
            }
            else if(cellType == CellType.Decorator)
            {
                // Convert Screen unit to game world unit
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, (1 << LayerMask.NameToLayer("Floor") | (1 << LayerMask.NameToLayer("Furniture")))))
                {
                    float surface = hit.collider.ClosestPointOnBounds(hit.point).y;
                    tempStructure.transform.position = new Vector3(hit.point.x, surface, hit.point.z);
                }

                if (Input.GetKeyDown((KeyCode)'r'))
                {
                    tempStructure.transform.Rotate(Vector3.up, 90.0f);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTemporaryStructure(Vector3Int.RoundToInt(tempStructure.transform.position), tempStructure, cellType);
                }
            }
            else if(cellType == CellType.WallDecorator)
            {

                //CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
                //cameraManager.MoveCamera();
                //cameraManager.DecorateLeftWall();

                // Convert Screen unit to game world unit
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, (1 << LayerMask.NameToLayer("Wall"))))
                {
                    tempStructure.transform.position = new Vector3(hit.point.x, hit.point.y, 0f);

                }

                if (Input.GetKeyDown((KeyCode)'r'))
                {
                    tempStructure.transform.Rotate(Vector3.up, 90.0f);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    // TODO
                    PlaceTemporaryStructure(Vector3Int.RoundToInt(tempStructure.transform.position), tempStructure, cellType);
                }
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
        return furnitureController.IfPlaceable;
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

    }

    internal void FreePosition(Vector3Int pos)
    {
        diningRoomGrid.SetGridStatus(pos.x, pos.z, 0);
    }

    // Display a preview of the object player trying to place
    public void ShowTemporalObject(GameObject item, CellType cellType, float rotation = 0)
    {
        CancelCurrentMovement();
        Vector3 pos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
        tempStructure = Instantiate(item, pos, Quaternion.Euler(0, rotation, 0));
        furnitureController = tempStructure.GetComponent<FurnitureController>();
        this.cellType = cellType;
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
