using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject bagUI;
    public UI_BagManager uI_BagManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSettingMenu()
    {
        menuUI.SetActive(!menuUI.activeInHierarchy);
    }

    public void ToggleBagMenu()
    {
        bagUI.SetActive(!bagUI.activeInHierarchy);
        uI_BagManager.RefreshInventoryItems();
    }
}
