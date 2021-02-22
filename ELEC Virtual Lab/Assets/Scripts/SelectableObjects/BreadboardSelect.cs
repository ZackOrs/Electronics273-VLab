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
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
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
        Globals.mouseClickAction = Globals.MouseClickAction.NoClick;

        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        CursorStyle.breadbBoardItemSelectedClickCount = 0;
    }


    private void ClearInventoryList()
    {
        Debug.Log("Destroy Count: " + InventoryListContent.transform.childCount);
        for (int i = 0; i < InventoryListContent.transform.childCount; i++)
        {
            Destroy(InventoryListContent.transform.GetChild(i).gameObject);
        }
    }
    private void CreateInventoryList()
    {
        Debug.Log("Create Count: " + Globals.inventoryItems.Count);
        for (int i = 0; i < Globals.inventoryItems.Count; i++)
        {
            if (!(Globals.inventoryItems[i] as SpawnableItem).isPlaced)
            {
                InventoryItemButton.GetComponentInChildren<TMP_Text>().text = (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString();
                Debug.Log("Item is: " + (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString() +
                (Globals.inventoryItems[i] as SpawnableItem).itemValue.ToString());


                switch ((Globals.inventoryItems[i] as SpawnableItem).itemName)
                {
                    case Globals.AvailableItems.Wire:
                        InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().color = Resources.Load<Image>(
                        (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString() +
                        (Globals.inventoryItems[i] as SpawnableItem).itemValue.ToString()).color;

                        InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().sprite = Resources.Load<Image>(
                        (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString() +
                        (Globals.inventoryItems[i] as SpawnableItem).itemValue.ToString()).sprite;
                        break;

                    case Globals.AvailableItems.Resistor:
                        InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().color = Resources.Load<Image>(
                        (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString() +
                        (Globals.inventoryItems[i] as SpawnableItem).itemValue.ToString()).color;

                        InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().sprite = Resources.Load<Image>(
                        (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString() +
                        (Globals.inventoryItems[i] as SpawnableItem).itemValue.ToString()).sprite;
                        break;

                    case Globals.AvailableItems.Capacitor:
                        InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().color = Resources.Load<Image>(
                            (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString()).color;

                        InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().sprite = Resources.Load<Image>(
                             (Globals.inventoryItems[i] as SpawnableItem).itemName.ToString()).sprite;

                        break;


                    default:
                        InventoryItemButton.transform.Find("ButtonImage").GetComponentInChildren<Image>().color = Resources.Load<Image>("Wire0").color;
                        break;
                }
                var buttonListButton = Instantiate(InventoryItemButton, InventoryListContent.transform);
                buttonListButton.GetComponent<InventoryItemClick>().Item = Globals.inventoryItems[i];
            }

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