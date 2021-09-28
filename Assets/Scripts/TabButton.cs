using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;
    public UI_BagManager uI_BagManager;
    public UI_StoreManager uI_StoreManager;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
    
    public void Select(string furniture)
    {
        string parentName = this.transform.parent.parent.parent.name;
        if(parentName == "Inventory")
        {
            uI_BagManager.RefreshInventoryItems(furniture);
        }
        else
        {
            //TODO: Store manager
            uI_StoreManager.RefreshStoreItems(furniture);
        }
    }

    public void Deselect()
    {

    }
}
