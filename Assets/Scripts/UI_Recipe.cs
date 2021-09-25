using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using System;

public class UI_Recipe : MonoBehaviour
{

    public RecipeManager recipeManager;
    public Sprite lockIcon;
    private Transform pageTransform;
    private Dictionary<string, RectTransform> posDict = new Dictionary<string, RectTransform>();

    private void Awake()
    {
        pageTransform = transform.Find("ItemList");
        float itemSlotCellSize = 300f;
        int x = 0;
        int y = 0;

        foreach (KeyValuePair<string, RecipeManager.Recipe> item in recipeManager.recipeDict)
        {


            RectTransform recipeRectTransform = Instantiate(pageTransform, this.transform).GetComponent<RectTransform>();
            recipeRectTransform.gameObject.SetActive(true);
            recipeRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                recipeManager.UnlockRecipe(item.Key);
                ChangeStatus(item.Key);
            };
            recipeRectTransform.anchoredPosition = new Vector2(-220 + x * itemSlotCellSize, 43 + y * itemSlotCellSize);
            Image image = recipeRectTransform.Find("Image").GetComponent<Image>();
            Image lockImage = recipeRectTransform.Find("LockImage").GetComponent<Image>();
            image.sprite = recipeManager.recipeIconDict[item.Key];

            TextMeshProUGUI name = recipeRectTransform.Find("Name").GetComponent<TextMeshProUGUI>();
            name.text = item.Key.Replace(@"_", " ");

            if (item.Value.ifUnlocked == false)
            {
                TextMeshProUGUI cost = recipeRectTransform.Find("Price").GetComponent<TextMeshProUGUI>();
                cost.text = item.Value.cost.ToString();
                lockImage.sprite = lockIcon;
            }

            posDict[item.Key] = recipeRectTransform;

            x++;
            if (x == 5)
            {
                y++;
                x = 0;
            }
        }
    }

    private void ChangeStatus(string name)
    {
        posDict[name].Find("Price").GetComponent<TextMeshProUGUI>().text = "Unlocked";
    }

}
