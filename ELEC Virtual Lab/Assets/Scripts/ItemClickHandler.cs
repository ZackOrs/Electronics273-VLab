using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public SpawnableItem spawnableItem;

    public static bool isBBSlot = false;

    
    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CursorStyle.breadbBoardItemSelectedClickCount > 0)
        {
            StartCoroutine(WaitForClick());
        }
    }

    public void ItemClicked()
    {
        if (CursorStyle.breadbBoardItemSelectedClickCount == 0)
        {
            ItemClickToHandle();
        }
    }

    private void ItemClickToHandle()
    {

        switch (spawnableItem.itemName)
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
        CursorStyle.breadbBoardItemSelectedClickCount = 1;

    }
    private void ResistorClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }
    private void CapacitorClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }

    private IEnumerator WaitForClick()
    {
        switch (CursorStyle.breadbBoardItemSelectedClickCount)
        {
            case 1:               
                if (Input.GetMouseButtonDown(0))
                {
                    CheckIfBBSlot();
                    if (isBBSlot)
                    {
                        Debug.Log("First clicked");
                        // CursorStyle.breadbBoardItemSelectedClickCount = 2;
                    }
                }
                break;

            case 2:
                if (Input.GetMouseButtonDown(0))
                {
                    CheckIfBBSlot();
                    if (isBBSlot)
                    {
                        Debug.Log("Second clicked");
                        // CursorStyle.breadbBoardItemSelectedClickCount = 0;
                    }
                }
                break;

            default:
                Debug.Log("Something is wrong");
                break;
        }

        yield return 0;
    }

    private void CheckIfBBSlot()
    {
        isBBSlot = false;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.transform.CompareTag("BBSlot"))
                {
                        isBBSlot = true;
                }
            }
        }
    }
}
