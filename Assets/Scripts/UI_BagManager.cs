using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BagManager : MonoBehaviour
{
    private BagManager bagManager;
    private Transform itemSlotTemplate;
    private Transform backGround;

    private void Awake()
    {
        itemSlotTemplate = transform.Find("ItemSlotTemplate");
        backGround = transform.Find("Background");
    }

    public void SetBagManager(BagManager bagManager)
    {
        this.bagManager = bagManager;
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

        foreach (Item item in bagManager.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, this.transform).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(247 + x * itemSlotCellSize, -231 + y * itemSlotCellSize);
            RawImage image = itemSlotRectTransform.Find("Image").GetComponent<RawImage>();
            image.texture = item.GetTexture2D();
            x++;
        }
    }

}
