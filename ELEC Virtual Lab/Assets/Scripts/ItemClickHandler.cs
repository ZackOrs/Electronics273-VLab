using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public static SpawnableItem spawnableItem;

    public static GameObject buttonClicked;

    float circuitCurrent;

    [SerializeField] GameObject _breadboardUI = null;
    [SerializeField] GameObject _voltmeter = null;
    [SerializeField] Image _wireImage = null;
    [SerializeField] Image _resistorImage = null;


    public static bool isBBSlotFree = false;
    private GameObject _pointA = null;
    private GameObject _pointB = null;
    public static List<GameObject> allSlots = new List<GameObject>();

    private List<SlotColumn> slotColumns = new List<SlotColumn>();

    int slotIDCounter = 0;

    private bool foundDeadEnd = true;

    void Start()
    {
        Debug.Log("Adding slots");
        allSlots.Clear();
        slotIDCounter = 0;
        for (int i = 0; i < _breadboardUI.transform.childCount; i++)
        {
            var child = _breadboardUI.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                if(child.GetComponent<Slot>().slotType != Globals.SlotType.voltmeterSlot)
                {
                    allSlots.Add(child.gameObject);
                    child.GetComponent<Slot>().slotID = slotIDCounter++;
                }

            }
        }

        slotColumns.Clear();
        for (int i = 0; i < slotIDCounter; i += 4)
        {
            var child = _breadboardUI.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                SlotColumn slotColumn = new SlotColumn(allSlots[i].GetComponent<Slot>(),
                allSlots[i + 1].GetComponent<Slot>(),
                allSlots[i + 2].GetComponent<Slot>(),
                allSlots[i + 3].GetComponent<Slot>());
                slotColumns.Add(slotColumn);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Globals.mouseClickAction > 0)
        {
            WaitForClick();
        }
        // // function attached to a button for testing
        // CalculateCircuit();
    }

    public void CalculateCircuit()
    {

        Debug.Log("Started Circuit Calculation");
        slotColumns.Clear();
        for (int i = 0; i < slotIDCounter; i += 4)
        {
            var child = _breadboardUI.transform.GetChild(i);
            child.GetComponent<Slot>().resistorAdded = false;
            if (child.CompareTag("BBSlot"))
            {
                SlotColumn slotColumn = new SlotColumn(allSlots[i].GetComponent<Slot>(),
                allSlots[i + 1].GetComponent<Slot>(),
                allSlots[i + 2].GetComponent<Slot>(),
                allSlots[i + 3].GetComponent<Slot>());
                slotColumns.Add(slotColumn);
            }
        }
        // for (int i = 0; i < slotColumns.Count; i++)
        // {
        //     // Debug.Log("Slot column: " + slotColumns[i].columnID + "\n slot connections: " + slotColumns[i].PrintAllSlotConnections());

        //     Debug.Log(slotColumns[i].printAllColumnConnections());
        // }

        UpdateSuccessors();

    do
    {
        RemoveDeadEnds();
    }while(foundDeadEnd);


        CalculateElectricalData();
        //  for (int i = 0; i < slotColumns.Count; i++)
        // {
        //     Debug.Log(slotColumns[i].printAllColumnConnections());
        // }

        Debug.Log("***** DONE CALCULATION *****");

        Debug.Log("Updating VoltMeter");

        _voltmeter.GetComponent<Voltmeter>().UpdateTerminals();
    }

    private void UpdateSuccessors()
    {
        for (int i = 0; i < slotColumns.Count; i++)
        {
            SlotColumn currentSlot = slotColumns[i];
            if (currentSlot.columnID == 4 || currentSlot.columnID == 5)
                continue;
            if (slotColumns[i].connectedToPower)
            {
                for (int j = 0; j < slotColumns[i].columnConnections.Count; j++)
                {
                    // Debug.Log("Column connection is: " + slotColumns[currentSlot.columnConnections[j]].columnID);
                    slotColumns[currentSlot.columnConnections[j]].connectedToPower = true;
                }
            }
        }

        for (int i = slotColumns.Count - 1; i > 0; i--)
        {
            SlotColumn currentSlot = slotColumns[i];
            if (currentSlot.columnID == 4 || currentSlot.columnID == 5)
                continue;
            if (slotColumns[i].connectedToPower)
            {
                for (int j = 0; j < slotColumns[i].columnConnections.Count; j++)
                {
                    // Debug.Log("Column connection is: " + slotColumns[currentSlot.columnConnections[j]].columnID);
                    slotColumns[currentSlot.columnConnections[j]].connectedToPower = true;
                }
            }
        }

        for (int i = 0; i < slotColumns.Count; i++)
        {
            SlotColumn currentSlot = slotColumns[i];
            if (currentSlot.columnID == 4 || currentSlot.columnID == 5)
                continue;
            if (slotColumns[i].connectedToGround)
            {
                for (int j = 0; j < slotColumns[i].columnConnections.Count; j++)
                {
                    // Debug.Log("Column connection is: " + slotColumns[currentSlot.columnConnections[j]].columnID);
                    slotColumns[currentSlot.columnConnections[j]].connectedToGround = true;
                }
            }
        }

        for (int i = slotColumns.Count - 1; i > 0; i--)
        {
            SlotColumn currentSlot = slotColumns[i];
            if (currentSlot.columnID == 4 || currentSlot.columnID == 5)
                continue;
            if (slotColumns[i].connectedToGround)
            {
                for (int j = 0; j < slotColumns[i].columnConnections.Count; j++)
                {
                    // Debug.Log("Column connection is: " + slotColumns[currentSlot.columnConnections[j]].columnID);
                    slotColumns[currentSlot.columnConnections[j]].connectedToGround = true;
                }
            }
        }
    }

    private void RemoveDeadEnds()
    {
        foundDeadEnd = false;
        for (int i = 0; i < slotColumns.Count; i++)
        {
            SlotColumn currentSlot = slotColumns[i];
            if (currentSlot.columnID == 4 || currentSlot.columnID == 5)
                continue;
            if (slotColumns[i].columnConnections.Count == 1)
            {
                // Debug.Log("Deadend: "+ slotColumns[i].columnID);
                slotColumns[i].columnConnections.Clear();
                RemoveConnectionSlot(slotColumns[i].columnID);
                slotColumns[i].isDeadEnd = true;
                foundDeadEnd= true;
            }
        }
    }

    private void RemoveConnectionSlot(int slotIDToRemove)
    {
        for(int i = 0; i < slotColumns.Count; i++)
        {
            if(slotColumns[i].columnConnections.Contains(slotIDToRemove))
            {
                // Debug.Log("Column: "+ slotColumns[i].columnID + " Removing connection: "+ slotIDToRemove);
                slotColumns[i].columnConnections[slotIDToRemove] = -1;
            }              
        }
    }

    private void CalculateElectricalData()
    {
        float resistanceTotal = 0;
        for(int i = 0 ; i < allSlots.Count; i++)
        {
            allSlots[i].GetComponent<Slot>().resistorAdded = false;
        }
        for (int i = 0; i < slotColumns.Count; i++)
        {
            if (slotColumns[i].connectedToPower && slotColumns[i].connectedToGround)
            {
                Debug.Log("Checking res for column: "+ slotColumns[i].columnID);
                float columnResistance = slotColumns[i].ResistorVal();
                resistanceTotal += columnResistance;
                Debug.Log("DONE for column: "+ slotColumns[i].columnID + " Value of: " + columnResistance);
               
            }
        }
        Debug.Log("Res Total: " + resistanceTotal);
        if(resistanceTotal > 0)
        {
            circuitCurrent = slotColumns[5].voltage / resistanceTotal;
        }
        else
        {
            circuitCurrent = 99999;
        }

        Debug.Log("Current: "+ circuitCurrent);


        float prevVoltage = slotColumns[5].voltage;
        for (int i = 0; i < slotColumns.Count; i++)
        {
            if (slotColumns[i].connectedToPower && slotColumns[i].connectedToGround)
            {
                slotColumns[i].ChangeAllVoltages(prevVoltage);
                float voltageDrop = circuitCurrent * slotColumns[i].resistance;
                prevVoltage -= voltageDrop;
                Debug.Log("voltage drop:" + voltageDrop);
            }
        }
    }


    public static void ItemClicked()
    {
        if (Globals.mouseClickAction == Globals.MouseClickAction.NoClick && spawnableItem != null)
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
        Globals.mouseClickAction = Globals.MouseClickAction.TwoClicks_FirstClick;
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


                        _pointB.GetComponent<Slot>().slotPair = _pointA;
                        _pointA.GetComponent<Slot>().slotPair = _pointB;
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

        Image placeItem = null;


        switch (spawnableItem.itemName)
        {
            case (Globals.AvailableItems.Wire):
                placeItem = Instantiate(_wireImage, _breadboardUI.transform);
                placeItem.GetComponent<Image>().color = Resources.Load<Image>(
                spawnableItem.itemName.ToString() +
                spawnableItem.itemValue.ToString()).color;

                break;

            case (Globals.AvailableItems.Resistor):
                placeItem = Instantiate(_resistorImage, _breadboardUI.transform);
                break;

            default:
                placeItem = Instantiate(_wireImage, _breadboardUI.transform);
                break;
        }
        if (spawnableItem.itemName == Globals.AvailableItems.Wire) // Will colour the wire
        {


        }

        float distance = Vector2.Distance(_pointA.transform.position, _pointB.transform.position);
        Debug.Log("Distance: " + distance);
        distance = distance * 0.71f; //Have it be slightly shorter so it gets the centers

        float rotation = AngleBetweenVector2(_pointA.transform.position, _pointB.transform.position);
        float posX = PositionXForLine(_pointA.transform.position, _pointB.transform.position);
        float posY = PositionYForLine(_pointA.transform.position, _pointB.transform.position);

        placeItem.transform.position = new Vector2(posX, posY);
        placeItem.rectTransform.sizeDelta = new Vector2(distance, placeItem.rectTransform.sizeDelta.y);
        placeItem.transform.Rotate(0, 0, rotation, Space.Self);

        //reset the points
        _pointA = null;
        _pointB = null;
    }

    private void ResetWire()
    {
        _wireImage.transform.position = new Vector3(0, 0, 0);
        _wireImage.transform.Rotate(0, 0, 0, Space.World);
        _wireImage.rectTransform.sizeDelta = new Vector2(25, _wireImage.rectTransform.sizeDelta.y);
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 difference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
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

                    if (Globals.mouseClickAction == Globals.MouseClickAction.TwoClicks_FirstClick)
                    {
                        _pointA = results[i].gameObject;
                    }
                    else if (Globals.mouseClickAction == Globals.MouseClickAction.TwoClicks_SecondClick)
                    {
                        _pointB = results[i].gameObject;
                    }
                }
            }
        }
    }
}