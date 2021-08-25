using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New InventoryData", menuName = "Inventory Data")]

public class InventoryManager : ScriptableObject
{
    private int money = 0;

    //public class Ingredient
    //{
    //    public string name;
    //    public int amount;
    //    public Ingredient() { }
    //    public Ingredient(string name, int amount)
    //    {
    //        this.name = name;
    //        this.amount = amount;
    //    }
    //}

    Dictionary<string, int> ingredientInventory = new Dictionary<string, int>();

    public int Money
    {
        get
        {
            return money;
        }
    }

    public Dictionary<string, int> IngredientInventory
    {
        get
        {
            return ingredientInventory;
        }
    }

    public void ChangeMoney(int amount)
    {
        money += amount;
    }


    public void AddIngredient(string name, int amount = 1)
    {
        if (ingredientInventory.ContainsKey(name))
        {
            ingredientInventory[name] += amount;
        }
        else
        {
            ingredientInventory[name] = amount;
        }
    }
    
}
