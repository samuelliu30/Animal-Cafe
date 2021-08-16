using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;
    private Vector2 camMovementVector;

    [SerializeField]
    Camera mainCamera;

    public LayerMask groudMask;

    public Vector2 CamMovementVector
    {
        get { return camMovementVector; }
    }

    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
    }

    private Vector3Int? RaycastGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groudMask))
        {
            Vector3Int postionInt = Vector3Int.RoundToInt(hit.point);
            return postionInt;
        }
        else
        {
            return null;
        }
    }

    private void CheckClickHoldEvent()
    {
        throw new NotImplementedException();
    }

    private void CheckClickUpEvent()
    {
        throw new NotImplementedException();
    }

    private void CheckClickDownEvent()
    {
        throw new NotImplementedException();
    }
}
