using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 initialPos;
    private Quaternion initialRotation;
    private Vector3 leftDoorPos;
    private Vector3 leftOffset = new Vector3(0.2f, 1.81f, 1.6f);
    private Vector3 rightDoorPos;
    private Vector3 rightOffset = new Vector3(0, 0, -1);
    private Quaternion leftDoorRotation = Quaternion.Euler(2.922f, -181.031f, 0f);
    private Quaternion rightDoorRotation = Quaternion.Euler(0, -90, 0);
    private float cameraSpeed = 8.0f;
    private bool move;
    private bool leftWall;
    private bool rightWall;
    private bool goBack;
    private bool ifRotate;
    private Vector3 rotation;
    private Vector3 floorCenter = new Vector3(2,3,2);
    private bool ifZoom = false;


    void Start()
    {
        initialPos = this.transform.position;
        initialRotation = this.transform.rotation;
        //leftDoorPos = GameObject.FindGameObjectWithTag("LeftWall").transform.position + leftOffset;
        rightDoorPos = leftDoorPos + rightOffset;
        move = false;
    }
    private void Update()
    {

        if (!move)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, initialPos, cameraSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, initialRotation, cameraSpeed * Time.deltaTime);
            if (this.transform.position == initialPos && this.transform.rotation == initialRotation)
            {
                move = false;
            }
        }
        else if (leftWall)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, leftDoorPos, cameraSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, leftDoorRotation, cameraSpeed * Time.deltaTime);
            //this.GetComponent<Camera>().orthographicSize -= 0.5f * Time.deltaTime;
            if (this.transform.position == leftDoorPos && (this.transform.rotation == leftDoorRotation))
            {
                leftWall = false;
            }
        }
        //else if (rightWall)
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position, rightDoorPos, cameraSpeed * Time.deltaTime);
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rightDoorRotation, cameraSpeed * Time.deltaTime);
        //    if (this.transform.position == rightDoorPos && this.transform.rotation == rightDoorRotation)
        //    {
        //        rightWall = false;
        //    }
        //}

        if (ifRotate)
        {
            transform.eulerAngles += rotation * cameraSpeed * Time.deltaTime;
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(this.transform.eulerAngles + rotation), cameraSpeed*Time.deltaTime);
            ifRotate = false;

            // Debug: set move to true so camera won't reset
            move = true;
        }

    }
    public void MoveCamera()
    {
        move = !move;
        if (!move)
        {
            leftWall = false;
            rightWall = false;
        }
    }

    public void DecorateLeftWall()
    {
        leftWall = true;
    }
    public void DecorateRightWall()
    {
        rightWall = true;
    }

    public bool IfDecorate()
    {
        return move;
    }

    public void CameraRotate(Vector3 rotation)
    {
        ifRotate = true;

        // TODO: Converts from screen space(mouse input) to world space
        float pixelPerDegreeX = Screen.width / 180f;
        float pixelPerDegreeY = Screen.height / 180f;
        this.rotation = new Vector3(rotation.x * pixelPerDegreeY, rotation.y * pixelPerDegreeX, 0f);
    }


    public void ZoomIn()
    {
        if (!ifZoom)
        {
            this.GetComponent<Camera>().orthographicSize -= 5;
            ifZoom = !ifZoom;
        }
        else
        {
            this.GetComponent<Camera>().orthographicSize += 5;
            ifZoom = !ifZoom;
        }
    }

}
