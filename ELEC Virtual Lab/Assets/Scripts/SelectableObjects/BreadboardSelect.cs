using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreadboardSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";
    public GameObject BreadboardPanel;
    public GameObject InventoryListContent;
    public GameObject InventoryItemButton;


    //THIS IS A BAD SOLUTION, NEED TO GET RID OF IT
    public static int index;
    
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
        void Update()
    {
        if(Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {
                CancelButton();
        }
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
        ClearInventoryList();
        BreadboardPanel.SetActive(false);

        Globals.showCrosshair = true;
        Globals.menuOpened = false;

        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        CursorStyle.breadbBoardItemSelectedClickCount = 0;
    }


    private void ClearInventoryList()
    {
        Debug.Log("Destroy Count: " + InventoryListContent.transform.childCount);
        for(int i=0 ; i < InventoryListContent.transform.childCount; i++)
        {
            Destroy(InventoryListContent.transform.GetChild(i).gameObject);
        }
    }
    private void CreateInventoryList()
    {
        Debug.Log("Create Count: " + Globals.inventoryItems.Count);
        for(int i = 0 ; i < Globals.inventoryItems.Count ; i++)
        {   
            InventoryItemButton.GetComponentInChildren<TMP_Text>().text = (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString();
            InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().color = Resources.Load<Image>("Red").color;
            InventoryItemButton.GetComponent<InventoryItemClick>().Item = Globals.inventoryItems[i];
            Debug.Log(InventoryItemButton.GetComponent<InventoryItemClick>().Item.itemID);

            //THIS IS A BAD SOLUTION BUT I COULDN'T GET OTHER THINGS TO WORK!!!
            index = i;


            Instantiate(InventoryItemButton,InventoryListContent.transform);
            
            //InventoryItemClick.transform.GetChild(i).GetComponent<ItemClickHandler>().spawnableItem = Globals.inventoryItems[i];
            // ItemClickHandler.spawnableItem = Globals.inventoryItems[i];
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