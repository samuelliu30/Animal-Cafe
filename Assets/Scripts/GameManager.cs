using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;
    public FurnitureManager furnitureManager;
    public DecorationManager decorationManager;
    public JsonManager jsonManager;

    private float spawnRate = 1.5f;
    private bool isStoreOpen = true;

    [SerializeField] CustomerManager customerManager;
    [SerializeField] Text moneyBoard;
    [SerializeField] InventoryManager InventoryData;
    [SerializeField] PlacementManager placementManager;
    [SerializeField] CameraManager cameraManager;
    public UI_BagManager uiBagManager;
    [SerializeField] ItemWorld itemWorld;

    public BagManager bagManager;

    IEnumerator SpawnTarget()
    {
        while (isStoreOpen)
        {
            yield return new WaitForSeconds(spawnRate);
            //customerManager.SpawnCustomer();
        }

    }

    private void Awake()
    {
        bagManager = new BagManager();
    }
    private void Start()
    {
        //LoadGame();
        inputManager.OnMouseClick += HandleMouseClick;
        inputManager.OnCameraRotation += HandleCameraRotate;
        StartCoroutine(SpawnTarget());
        uiBagManager.SetBagManager(bagManager);
        furnitureManager.SetBagManager(bagManager);
    }

    private void Update()
    {
        DisplayMoney(InventoryData);
    }

    private void DisplayMoney(InventoryManager MoneyData)
    {
        moneyBoard.text = InventoryData.Money.ToString();
    }

    // Deprecated
    private void HandleMouseClick(Vector3Int pos)
    {
        Debug.Log(pos);
        
        /*
        if (furnitureManager.IfPlaceable())
        {
            furnitureManager.PlaceFurniture(pos);
        }
        else if (placementManager.IfMove())
        {
            furnitureManager.PickUp();
        }
        else if (cameraManager.IfDecorate())
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                GameObject c = hit.collider.gameObject;
                if (c.tag == "LeftWall")
                {
                    cameraManager.DecorateLeftWall();
                }
                else if (c.tag == "RightWall")
                {
                    cameraManager.DecorateRightWall();
                }
            }
        }
        else if (itemWorld.IfStore())
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                GameObject c = hit.collider.gameObject;
                if (c.name.Contains("Chair"))
                {
                    bagManager.AddItem(new Item { itemType = Item.ItemType.Chair, name = "chair", amount = 1 });
                    furnitureManager.Store(c);
                }
                else if (c.name.Contains("Table"))
                {
                    bagManager.AddItem(new Item { itemType = Item.ItemType.Table, name = "table", amount = 1 });
                    furnitureManager.Store(c);
                }
            }
        }
        */

    }


    private void HandleCameraRotate(Vector3 rot)
    {
        cameraManager.CameraRotate(rot);
    }

    public void LoadGame()
    {
        jsonManager.LoadSetupData();
    }

    public void SaveGame()
    {
        jsonManager.SaveSetupData();
    }
}
