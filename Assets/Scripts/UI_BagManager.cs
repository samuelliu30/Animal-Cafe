using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using System;

public class UI_BagManager : MonoBehaviour
{
    private BagManager bagManager;
    private Transform itemSlotTemplate;
    private Transform tableTransform;
    private Transform inventoryBackground;

    private Transform backGround;
    [SerializeField]
    FurnitureManager furnitureManager;

    private Dictionary<string, Item> tmpDict;

    public GameObject inventoryArea;

    private void Awake()
    {
        //itemSlotTemplate = transform.Find("ItemSlotTemplate");
        //backGround = transform.Find("Background");
        tableTransform = transform.Find("TableItem");
        inventoryBackground = transform.Find("InventoryBackground");
    }

    public void SetBagManager(BagManager bagManager)
    {
        this.bagManager = bagManager;

        bagManager.OnItemListChanged += BagManager_OnItemListchanged;
        //RefreshBagItems();
        //RefreshInventoryItems();
    }

    private void BagManager_OnItemListchanged(object sender, EventArgs e)
    {
        //RefreshBagItems();
        //RefreshInventoryItems();
    }

    public void RefreshBagItems()
    {
        foreach (Transform child in this.transform)
        {
            if (child == itemSlotTemplate || child == backGround) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 90f;

        foreach (KeyValuePair<string, Item> item in bagManager.ItemDict)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, this.transform).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                furnitureManager.fromInventory = true;
                furnitureManager.ChangePlacement(item.Value.name);
            };
            itemSlotRectTransform.anchoredPosition = new Vector2(-130 + x * itemSlotCellSize, -50 + y * itemSlotCellSize);
            RawImage image = itemSlotRectTransform.Find("Image").GetComponent<RawImage>();
            image.texture = item.Value.GetTexture2D();
            TextMeshProUGUI text = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.SetText(item.Value.amount.ToString());
            x++;
        }
    }

    public void RefreshInventoryItems(string furniture = "default")
    {
        switch (furniture)
        {
            case "table":
                break;
            case "chair":
                break;
            default:
                tmpDict = bagManager.TableDict;
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

        foreach (KeyValuePair<string, Item> item in tmpDict)
        {
            RectTransform tableRectTransform = Instantiate(tableTransform, this.transform).GetComponent<RectTransform>();
            tableRectTransform.gameObject.SetActive(true);
            tableRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                furnitureManager.fromInventory = true;
                furnitureManager.ChangePlacement(item.Value.name);
                inventoryArea.SetActive(false);
            };
            tableRectTransform.anchoredPosition = new Vector2(-286 + x * itemSlotCellSize, 43 + y * itemSlotCellSize);
            RawImage image = tableRectTransform.Find("Image").GetComponent<RawImage>();
            image.texture = item.Value.GetTexture2DByName();
            TextMeshProUGUI text = tableRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.SetText(item.Value.amount.ToString());
            x++;
            if (x == 5)
            {
                y++;
                x = 0;
            }
        }
    }

}
