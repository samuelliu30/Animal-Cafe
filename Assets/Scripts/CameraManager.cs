using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 initialPos;
    private Quaternion initialRotation;
    private Vector3 leftDoorPos;
    private Vector3 leftOffset;
    private Vector3 rightDoorPos;
    private Vector3 rightOffset;
    private Quaternion leftDoorRotation;
    private Quaternion rightDoorRotation;
    private Quaternion temp;
    private Quaternion temp1;
    private Quaternion temp2;
    private float cameraSpeed = 8.0f;
    private bool move;
    private bool leftWall;
    private bool rightWall;
    private bool goBack;

    void Start()
    {
        initialPos = this.transform.position;
        initialRotation = this.transform.rotation;
        leftOffset = new Vector3(0.2f, 1.81f, 1.6f);
        leftDoorPos = GameObject.FindGameObjectWithTag("LeftWall").transform.position + leftOffset;
        rightOffset = new Vector3(0, 0, -1);
        rightDoorPos = leftDoorPos + rightOffset;
        leftDoorRotation = Quaternion.Euler(2.922f, -181.031f, 0f);
        rightDoorRotation = Quaternion.Euler(0, -90, 0);
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
        else if (rightWall)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, rightDoorPos, cameraSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rightDoorRotation, cameraSpeed * Time.deltaTime);
            if (this.transform.position == rightDoorPos && this.transform.rotation == rightDoorRotation)
            {
                rightWall = false;
            }
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
}
