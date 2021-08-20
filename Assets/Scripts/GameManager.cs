using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;
    public FurnitureManager furnitureManager;
    public JsonManager jsonManager;

    private float spawnRate = 1.5f;
    private bool isStoreOpen = true;

    [SerializeField] CustomerManager customerManager;
    [SerializeField] TextMeshProUGUI moneyBoard;
    [SerializeField] InventoryManager MoneyData;
    [SerializeField] PlacementManager placementManager;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] UI_BagManager uiBagManager;
    [SerializeField] ItemWorld itemWorld;

    private BagManager bagManager;



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
        StartCoroutine(SpawnTarget());
        uiBagManager.SetBagManager(bagManager);
    }

    private void Update()
    {
        DisplayMoney(MoneyData);
    }

    private void DisplayMoney(InventoryManager MoneyData)
    {
        moneyBoard.text = "Money: " + MoneyData.Money;
    }

    private void HandleMouseClick(Vector3Int pos)
    {
        Debug.Log(pos);
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
                    bagManager.AddItem(new Item { itemType = Item.ItemType.Chair, name = "chair", amount = 1});
                    uiBagManager.RefreshBagItems();
                    furnitureManager.Store(c);
                }
                else if (c.name.Contains("Table"))
                {
                    bagManager.AddItem(new Item { itemType = Item.ItemType.Table, name = "table", amount = 1 });
                    uiBagManager.RefreshBagItems();
                    furnitureManager.Store(c);
                }
            }
        }
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
