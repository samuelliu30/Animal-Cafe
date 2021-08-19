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

    IEnumerator SpawnTarget()
    {
        while (isStoreOpen)
        {
            yield return new WaitForSeconds(spawnRate);
            customerManager.SpawnCustomer();
        }

    }


    private void Start()
    {
        inputManager.OnMouseClick += HandleMouseClick;
        StartCoroutine(SpawnTarget());
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
