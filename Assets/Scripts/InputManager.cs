using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;
    public Action<Vector3> OnCameraRotation;
    private Vector2 camMovementVector;

    [SerializeField]
    Camera mainCamera;

    public LayerMask groudMask;
    public LayerMask wallMask;

    public Vector2 CamMovementVector
    {
        get { return camMovementVector; }
    }

    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckCameraRotationEvent();
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

    private Vector3Int? RaycastWall()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallMask))
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
        if(Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if(position != null)
            {
                OnMouseHold?.Invoke(position.Value);
            }
        }
    }

    private void CheckClickUpEvent()
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
            {
                OnMouseUp?.Invoke();
            }
        }
    }

    private void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
            {
                OnMouseClick?.Invoke(position.Value);
            }
        }
    }

    // Mobile: change input detection to two finger
    private void CheckCameraRotationEvent()
    {
        if (Input.GetMouseButton(1) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
            {
                // Take x axis input to y rotation and y aix input to x rotation because the camera will be rotate along that axis
                // ex.swiping left will cause the camera rotate along y axis to the left
                // Also inverted Y axis rotation
                Vector3 cameraRotation = new Vector3(-1 * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
                OnCameraRotation?.Invoke(cameraRotation);
            }
        }
    }

}
