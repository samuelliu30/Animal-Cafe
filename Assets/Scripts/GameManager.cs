using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;
    public FurnitureManager furnitureManager;

    private void Start()
    {
        inputManager.OnMouseClick += HandleMouseClick;
    }

    private void Update()
    {
        
    }

    private void HandleMouseClick(Vector3Int pos)
    {
        Debug.Log(pos);
        furnitureManager.PlaceFurniture(pos);
    }
}
