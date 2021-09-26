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
