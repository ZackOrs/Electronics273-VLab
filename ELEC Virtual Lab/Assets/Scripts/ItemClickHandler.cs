using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public static SpawnableItem spawnableItem;

    public static GameObject buttonClicked;

    [SerializeField] GameObject _breadboardUI = null;
    [SerializeField] Image _wireImage = null;

    public static bool isBBSlotFree = false;
    private GameObject _pointA = null;
    private GameObject _pointB = null;
    private List<GameObject> allSlots = new List<GameObject>();

    
    void Start()
    {
        Debug.Log("Adding slots");
        allSlots.Clear();
        
        for (int i = 0 ; i < _breadboardUI.transform.childCount ; i ++)
        {
            var child = _breadboardUI.transform.GetChild(i);
            if(child.CompareTag("BBSlot"))
            {
                allSlots.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Globals.mouseClickAction > 0)
        {
            Debug.Log("Waiting on click");
            WaitForClick();
        }
        // // function attached to a button for testing
        // CalculateCircuit();
    }

    public void CalculateCircuit()
    {
        Debug.Log("Slots number = " + allSlots.Count);
        for (int i = 0; i < allSlots.Count;i++)
        {
            if(allSlots[i].transform.GetComponent<Slot>().itemPlaced != null)
            {
                Debug.Log("Slot: " + i + "\tItem: "+ allSlots[i].transform.GetComponent<Slot>().itemPlaced.itemName.ToString());
            }          
        }
    }

    public static void ItemClicked()
    {
        if (Globals.mouseClickAction == Globals.MouseClickAction.NoClick && spawnableItem!=null)
        {
            Debug.Log("item clicked");
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
                        _pointA.GetComponent<Slot>().PlaceItem();
                        _pointA.GetComponent<Slot>().itemPlaced = spawnableItem;
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
                        _pointB.GetComponent<Slot>().PlaceItem();
                        _pointB.GetComponent<Slot>().itemPlaced = spawnableItem;
                        DrawLineBetweenPoints();
                        RemoveItemButtonInList();    
                    }
                }
                break;

            default:
                Debug.Log("Something is wrong");
                break;
        }
    }

    private void RemoveItemButtonInList()
    {
       spawnableItem.isPlaced = true;
       Destroy(buttonClicked);
    }

    private void DrawLineBetweenPoints()
    {  
        var placeItem = Instantiate(_wireImage,_breadboardUI.transform);

        if(spawnableItem.itemName == Globals.AvailableItems.Wire) // Will colour the wire
        {
            placeItem.GetComponent<Image>().color = Resources.Load<Image>(
                spawnableItem.itemName.ToString() + 
                spawnableItem.itemValue.ToString()).color;
        }

        float distance = Vector2.Distance(_pointA.transform.position,_pointB.transform.position);
        Debug.Log("Distance: " + distance);
        distance = distance * 0.68f; //Have it be slightly shorter so it gets the centers

        float rotation = AngleBetweenVector2(_pointA.transform.position,_pointB.transform.position);
        float posX = PositionXForLine(_pointA.transform.position,_pointB.transform.position);
        float posY = PositionYForLine(_pointA.transform.position,_pointB.transform.position);

        placeItem.transform.position = new Vector2(posX,posY);
        placeItem.rectTransform.sizeDelta = new Vector2(distance, placeItem.rectTransform.sizeDelta.y);
        placeItem.transform.Rotate(0,0,rotation,Space.Self);

        //reset the points
        _pointA = null;
        _pointB = null;
    }

    private void ResetWire()
    {
        _wireImage.transform.position = new Vector3(0,0,0);
        _wireImage.transform.Rotate(0,0,0,Space.World);
        _wireImage.rectTransform.sizeDelta = new Vector2(25, _wireImage.rectTransform.sizeDelta.y);
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
            Vector2 difference = vec2 - vec1;
            float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
            return Vector2.Angle(Vector2.right, difference) * sign;
    }

//Gets the center X between both points
    private float PositionXForLine(Vector2 vec1, Vector2 vec2)
    {
        float x1 = vec1.x;
        float x2 = vec2.x;
        return ((x1 + x2) / 2.0f);
    }

    //Gets the center Y between both points
    private float PositionYForLine(Vector2 vec1, Vector2 vec2)
    {
        float y1 = vec1.y;
        float y2 = vec2.y;
        return ((y1 + y2) / 2.0f);
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
                        _pointA = results[i].gameObject;
                    }
                    else if(Globals.mouseClickAction == Globals.MouseClickAction.TwoClicks_SecondClick)
                    {
                        _pointB = results[i].gameObject;
                    }
                }
            }
        }
    }
}