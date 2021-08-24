using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    private Transform itemSlotTemplate;
    private List<StoreItem> storeItems = new List<StoreItem>() { new StoreItem { name = "chair", price = 50 }, new StoreItem { name = "table", price = 100 } };
    [SerializeField]
    FurnitureManager furnitureManager;
    [SerializeField]
    InventoryManager MoneyData;

    private void Start()
    {
        itemSlotTemplate = transform.Find("ItemSlotTemplate");
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 90f;
        foreach (StoreItem storeItem in storeItems)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, this.transform).GetComponent<RectTransform>();
            itemSlotRectTransform.name = storeItem.name;
            itemSlotRectTransform.tag = "BuyFurniture";
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(247 + x * itemSlotCellSize, -231 + y * itemSlotCellSize);
            RawImage image = itemSlotRectTransform.Find("Image").GetComponent<RawImage>();
            image.texture = storeItem.GetTexture2DByName();
            TextMeshProUGUI text = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.SetText(storeItem.price.ToString());
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                BuyFurniture(storeItem);
            };
            x++;
        }
    }

    private void BuyFurniture(StoreItem storeItem)
    {
        if (MoneyData.Money >= storeItem.price)
        {
            MoneyData.ChangeMoney(-storeItem.price);
            furnitureManager.ChangePlacement(storeItem.name);
        }
        else
        {
            bool tmp = EditorUtility.DisplayDialog("Uh oh! Not enough money", "Do you want to use diamond to trade some gold?", "Sure!", "GTFO");
        }
    }

}
