using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RecipeManager
{

    //public  class recipe
    //{
    //    public string name;
    //    public int cost;
    //    public List<string> ingredients; 
    //}

    public class Recipe
    {
        public int cost;
        public int revenue;
        public bool ifUnlocked;
    }

    public Dictionary<string, Recipe> recipeDict = new Dictionary<string, Recipe>();
    public Dictionary<string, GameObject> recipeIcons = new Dictionary<string, GameObject>();

    [SerializeField] InventoryManager moneyData;

    public void LoadRecipes(Dictionary<string, Recipe>recipeData)
    {
        // Load all recipes from local
        recipeDict = recipeData;
    }

    public void UnlockRecipe(string name)
    {
        // Spend money to unlock a recipe
        if (moneyData.Money >= recipeDict[name].cost)
        {
            moneyData.ChangeMoney(recipeDict[name].cost);
            recipeDict[name].ifUnlocked = true;
        }
        else
        {
            //TODO
            Debug.Log("Not enough money");
        }
    }

}
