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
    private Transform backGround;
    [SerializeField]
    FurnitureManager furnitureManager;

    private void Awake()
    {
        itemSlotTemplate = transform.Find("ItemSlotTemplate");
        backGround = transform.Find("Background");
    }

    public void SetBagManager(BagManager bagManager)
    {
        this.bagManager = bagManager;

        bagManager.OnItemListChanged += BagManager_OnItemListchanged;
        RefreshBagItems();
    }

    private void BagManager_OnItemListchanged(object sender, EventArgs e)
    {
        RefreshBagItems();
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
                furnitureManager.ChangePlacement(item.Value.name);
            };
            itemSlotRectTransform.anchoredPosition = new Vector2(247 + x * itemSlotCellSize, -231 + y * itemSlotCellSize);
            RawImage image = itemSlotRectTransform.Find("Image").GetComponent<RawImage>();
            image.texture = item.Value.GetTexture2D();
            TextMeshProUGUI text = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.SetText(item.Value.amount.ToString());
            x++;
        }
    }

}
