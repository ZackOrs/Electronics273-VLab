using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public static SpawnableItem spawnableItem;

    [SerializeField] GameObject BreadboardUI;

    public static bool isBBSlotFree = false;
    private GameObject pointA = null;
    private GameObject pointB = null;

    
    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.mouseClickAction > 0)
        {
            WaitForClick();
        }
    }

    public static void ItemClicked()
    {
        if (Globals.mouseClickAction == 0 && spawnableItem!=null)
        {
             ItemClickToHandle();
        }
    }

    private static void ItemClickToHandle()
    {
        switch (spawnableItem.itemName)
        {
            case Globals.AvailableItems.Wire:
                WireClicked();
                break;
            case Globals.AvailableItems.Resistor:
                ResistorClicked();
                break;
            case Globals.AvailableItems.Capacitor:
                CapacitorClicked();
                break;
            default:
                Debug.Log("Click not recognized");
                break;
        }
    }

    private static void WireClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
        Globals.mouseClickAction = Globals.MouseClickAction.TwoClicks_FirstClick;
    }

    private static void ResistorClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }

    private static void CapacitorClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }

    private void WaitForClick()
    {
        switch (Globals.mouseClickAction)
        {
            case Globals.MouseClickAction.TwoClicks_FirstClick:
                if (Input.GetMouseButtonDown(0))
                {
                    CheckIfBBSlot();
                    if (isBBSlotFree)
                    {
                        Debug.Log("First Click GOOD");
                    }
                }
                break;

            case Globals.MouseClickAction.TwoClicks_SecondClick:
                if (Input.GetMouseButtonDown(0))
                {
                    CheckIfBBSlot();
                    if (isBBSlotFree)
                    {
                        Debug.Log("Second Click GOOD DRAWING LINE");
                        DrawLineBetweenPoints();
                    }
                }
                break;

            default:
                Debug.Log("Something is wrong");
                break;
        }
    }

    private void DrawLineBetweenPoints()
    {  

    }


    private void CheckIfBBSlot()
    {
        isBBSlotFree = false;
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
                    isBBSlotFree = results[i].gameObject.transform.GetComponent<Slot>().isFree;

                    if(Globals.mouseClickAction == Globals.MouseClickAction.TwoClicks_FirstClick)
                    {
                        pointA = results[i].gameObject;
                    }
                    else if(Globals.mouseClickAction == Globals.MouseClickAction.TwoClicks_SecondClick)
                    {
                        pointB = results[i].gameObject;
                    }
                }
            }
        }
    }
}