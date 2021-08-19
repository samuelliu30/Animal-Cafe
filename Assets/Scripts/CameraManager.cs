using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 leftDoorPos;
    private Vector3 leftOffset;
    private Quaternion leftDoorRotation;
    private float cameraSpeed = 5.0f;
    private bool move;
    private bool leftWall;

    void Start()
    {
        leftOffset = new Vector3(0.2f, 1.81f, 1.6f);
        leftDoorPos = GameObject.FindGameObjectWithTag("LeftWall").transform.position + leftOffset;
        leftDoorRotation = Quaternion.Euler(2.922f, 178.969f, 0f);
    }
    private void Update()
    {
        if (leftWall)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, leftDoorPos, cameraSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, leftDoorRotation, cameraSpeed * Time.deltaTime);
            if (this.transform.position == leftDoorPos && this.transform.rotation == leftDoorRotation)
            {
                leftWall = false;
            }
        }
        
    }
    public void MoveCamera()
    {
        move = true;
    }

    public void DecorateLeftWall()
    {
        leftWall = true;
    }

    public bool IfDecorate()
    {
        return move;
    }
}
