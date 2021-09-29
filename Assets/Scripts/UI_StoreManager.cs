using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using System;


public class UI_StoreManager : MonoBehaviour
{
    private Dictionary<string, Item> tmpDict;

    public GameObject inventoryArea;
    public FurnitureManager furnitureManager;
    public ItemAssets itemAssets;
    public UI_BagManager inventory;
    [SerializeField]
    InventoryManager moneyManager;

    private Transform itemSlotTemplate;
    private Transform tableTransform;
    private Transform inventoryBackground;
    private Transform backGround;


    private void Awake()
    {
        //itemSlotTemplate = transform.Find("ItemSlotTemplate");
        //backGround = transform.Find("Background");
        tableTransform = transform.Find("TableItem");
        inventoryBackground = transform.Find("InventoryBackground");
    }

    public void RefreshStoreItems(string furniture = "default")
    {
        ICollection<string> keyList = new List<string>();
        switch (furniture)
        {
            case "table":
                keyList = itemAssets.tablePool.Keys;
                break;
            case "chair":
                keyList = itemAssets.chairPool.Keys;
                break;
            case "decoration":
                keyList = itemAssets.decoPool.Keys;
                break;
            case "wall":
                keyList = itemAssets.wallPool.Keys;
                break;
            default:
                furniture = "table";
                keyList = itemAssets.tablePool.Keys;
                break;
        }
        foreach (Transform child in this.transform)
        {
            if (child == itemSlotTemplate || child == backGround || child == tableTransform || child == inventoryBackground) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 150f;

        foreach (object i in keyList)
        {
            RectTransform tableRectTransform = Instantiate(tableTransform, this.transform).GetComponent<RectTransform>();
            tableRectTransform.gameObject.SetActive(true);

            int cost = itemAssets.priceDict[i.ToString()];
            tableRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                if (cost < moneyManager.Money)
                {
                    moneyManager.ChangeMoney(-cost);
                    inventory.AddItemToBagmanager(i.ToString(), furniture);
                }
            };

            tableRectTransform.anchoredPosition = new Vector2(-286 + x * itemSlotCellSize, 43 + y * itemSlotCellSize);
            RawImage image = tableRectTransform.Find("Image").GetComponent<RawImage>();
            image.texture = itemAssets.itemPool[i.ToString()];
            TextMeshProUGUI text = tableRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.SetText(itemAssets.priceDict[i.ToString()].ToString());
            x++;
            if (x == 5)
            {
                y++;
                x = 0;
            }
        }
    }
}
