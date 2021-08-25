using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private bool store;
    private bool takeOut;
    private string furniture;
    [SerializeField] InventoryManager inventoryManager;

    public string Furniture
    {
        get 
        {
            return furniture;
        }
    }

    private void Start()
    {
        store = false;
        takeOut = false;
    }

    public bool IfStore()
    {
        return store;
    }

    public void StoreFurniture()
    {
        store = !store;
    }
     
    public bool IfTakeOut()
    {
        return takeOut;
    }

    public void TakeOutFurniture(string name)
    {
        takeOut = true;
        furniture = name;
    }

    public void BuyIngredient(string name, int amount = 1)
    {
        inventoryManager.AddIngredient(name, amount);
        Debug.Log(inventoryManager.IngredientInventory[name]);
    }

}
