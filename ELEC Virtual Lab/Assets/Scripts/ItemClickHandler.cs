using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public SpawnableItem spawnableItem;

    private bool doFirstClick = false;
    private bool doSecondClick = false;
    void start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doFirstClick || doSecondClick)
            WaitForClick();
    }

    public void ItemClicked()
    {
        // Debug.Log("Item component is: " + spawnableItem.itemName.ToString());
        ItemClickToHandle();
    }

    private void ItemClickToHandle()
    {
        
        switch(spawnableItem.itemName)
        {
            case Globals.availableItems.Wire:
                WireClicked();
                break;
            case Globals.availableItems.Resistor:
                ResistorClicked();
                break;
            case Globals.availableItems.Capacitor:
                CapacitorClicked();
                break;
            default:
                Debug.Log("Click not recognized");
                break;
        }
    }

    private void WireClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
        CursorStyle.breadbBoardItemSelectedClickCount++;
        doFirstClick = true;
        StartCoroutine("WaitForClick"); 
    }
        private void ResistorClicked()
    {
         Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }
        private void CapacitorClicked()
    {
         Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }


    private void WaitForClick()
    {
        if(doFirstClick)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("First clicked");
                doFirstClick = false;
                doSecondClick = true;
                CursorStyle.breadbBoardItemSelectedClickCount++;
            }
        }
        else if(doSecondClick)
        {
             Debug.Log("Second Not CLicked");
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("Second clicked");
                doSecondClick = false;
                CursorStyle.breadbBoardItemSelectedClickCount = 0;
            }
        }
        else
        {
            Debug.Log("Something is wrong");
        }
    }
}
