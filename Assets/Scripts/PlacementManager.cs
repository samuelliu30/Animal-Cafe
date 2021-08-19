using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;

    private GameObject tempStructure;
    private GameObject placementIndicator;
    public FurnitureManager furnitureManager;
    private bool move;
    private bool ifPlaceable;

    private void Start()
    {
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

                // Convert Vector3 to Vector3
                Vector3 pointInt = new Vector3(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y), Mathf.FloorToInt(hit.point.z));
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

    private void ChangeIndicatorColor(Vector3 pos)
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

    internal bool CheckIfPositionInBound(Vector3 pos)
    {
        if((pos.x > 0)&&(pos.x < width)&&(pos.z > 0)&&(pos.z < height)&&(Mathf.Abs(pos.y)<0.1f))
            return true;
        else
            return false;
    }

    internal bool CheckIfPositionIsFree(Vector3 pos)
    {
        // tempStructure.GetComponent<Collider>().
        return true;
    }

    internal void PlaceTemporaryStructure(Vector3 pos, GameObject item, CellType cellType)
    {
        Debug.Log("CUCK CUCK CUCK");
        GameObject newStructure = Instantiate(item, pos, tempStructure.transform.rotation);

        // Destroy Indicator
        GameObject.Destroy(tempStructure);
        GameObject.Destroy(placementIndicator);
    }

    internal void FreePosition(Vector3 pos)
    {
    }

    // Display a preview of the object player trying to place
    public void ShowTemporalObject(GameObject item, GameObject preview, float rotation = 0)
    {
        tempStructure = Instantiate(item, Input.mousePosition, Quaternion.Euler(0, rotation, 0));
        //Vector3 size = tempStructure.GetComponent<Renderer>().bounds.size;
        placementIndicator = Instantiate(preview, tempStructure.transform.position, Quaternion.Euler(0, rotation, 0));
    }

    internal void PlaceObject(Vector3 pos, string name, Quaternion rotation, CellType type)
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
        move = true;
    }

    void OnCollisionEnter(Collision collider)
    {
        ifPlaceable = false;
    }
    
    void OnCollisionExist(Collision collider)
    {
        ifPlaceable = true;
    }

}
