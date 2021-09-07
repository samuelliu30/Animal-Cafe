using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class RecipeManager : MonoBehaviour
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

        public Recipe(int cost, int revenue, bool ifUnlocked)
        {
            this.cost = cost;
            this.revenue = revenue;
            this.ifUnlocked = ifUnlocked;
        }
    }

    public SerializableDictionary<string, Sprite> recipeIconDict;


    public Dictionary<string, Recipe> recipeDict = new Dictionary<string, Recipe>();

    [SerializeField] InventoryManager moneyData;

    // Debug use
    private void Start()
    {
        recipeDict["coffee"] = new Recipe(1, 1, false);
        recipeDict["coffee_icecream"] = new Recipe(1, 1, false);
    }


    public void LoadRecipes(Dictionary<string, Recipe>recipeData)
    {
        // Load all recipes from local
        recipeDict = recipeData;
    }

    public void UnlockRecipe(string name)
    {
        if(recipeDict[name].ifUnlocked == false)
        {
            // Spend money to unlock a recipe
            if (moneyData.Money >= recipeDict[name].cost)
            {
                moneyData.ChangeMoney(-recipeDict[name].cost);
                recipeDict[name].ifUnlocked = true;
            }
            else
            {
                //TODO
                Debug.Log("Not enough money");
            }
        }

    }

}
