using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreadboardSelect : SelectableItemBase
{

    // [SerializeField] private string spawnableTag = "Spawnable";
    public GameObject BreadboardPanel;
    public GameObject InventoryList;
    public GameObject InvetoryItemButton;
    

    public override string Name
    {
        get
        {
            return "Breadboard";
        }
    }

    public override void onInteract()
    { 
        OpenBreadboardPanel();
    }

    public void OpenBreadboardPanel()
    {
        BreadboardPanel.SetActive(true);
        
        Globals.showCrosshair = false;
        Globals.menuOpened = true;
        Globals.showCrosshair = false;

        Time.timeScale = 0.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        CreateInventoryList();
    }     

    public void CloseBreadboardPanel()
    {
        BreadboardPanel.SetActive(false);

        Globals.showCrosshair = true;
        Globals.menuOpened = false;

        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void CreateInventoryList()
    {
        for(int i = 0 ; i < Globals.inventoryItems.Count ; i++)
        {
            
            InvetoryItemButton.GetComponentInChildren<TMP_Text>().text = (Globals.inventoryItems[i] as SpawnableItem).ItemName;
            InvetoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().color = Resources.Load<Image>("Red").color;
            Instantiate(InvetoryItemButton,InventoryList.transform);
        }
    }

    //Button functions
    public void CancelButton()
    {
        CloseBreadboardPanel();
    }

    public void ResetButton()
    {
        //TODO: IMPLEMENT CLEAR, probably add a tag to all created objects and then remove them
        Debug.Log("Clearing table");
    }

    public void ConfirmButton()
    {
        //TODO: Impletement confirm, probably make a list containing all objects and then create them as needed
        Debug.Log("Confirm Selection");
        CloseBreadboardPanel();
    }
}