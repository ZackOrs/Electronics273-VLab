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
    [SerializeField] GameObject _currentmeter = null;

    [SerializeField] Image _wireImage = null;
    [SerializeField] Image _resistorImage = null;
    [SerializeField] Image _capacitorImage = null;

    [SerializeField] GameObject _placedImages = null;

    public static bool isBBSlotFree = false;
    private GameObject _pointA = null;
    private GameObject _pointB = null;
    public static List<GameObject> breadboardSlots = new List<GameObject>();
    public static List<GameObject> allSlots = new List<GameObject>();

    private List<SlotColumn> slotColumns = new List<SlotColumn>();

    int breadboardSlotIDCounter = 0;
    int allSlotIDCounter = 0;


    int amMeterStart = -1;
    int amMeterEnd = -1;

    private bool foundDeadEnd = true;

    void Start()
    {
        Debug.Log("Adding slots");
        breadboardSlots.Clear();
        allSlots.Clear();
        breadboardSlotIDCounter = 0;
        allSlotIDCounter = 0;
        for (int i = 0; i < _breadboardUI.transform.childCount; i++)
        {
            var child = _breadboardUI.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                if (child.GetComponent<Slot>().slotType == Globals.SlotType.defaultSlot ||
                child.GetComponent<Slot>().slotType == Globals.SlotType.groundSlot ||
                child.GetComponent<Slot>().slotType == Globals.SlotType.startSlot)
                {
                    breadboardSlots.Add(child.gameObject);
                    allSlots.Add(child.gameObject);

                    child.GetComponent<Slot>().slotID = breadboardSlotIDCounter++;
                    allSlotIDCounter++;
                }
            }
        }


        for (int i = 0; i < _voltmeter.transform.childCount; i++)
        {
            var child = _voltmeter.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                allSlots.Add(child.gameObject);
                child.GetComponent<Slot>().slotID = allSlotIDCounter++;
            }
        }

        for (int i = 0; i < _currentmeter.transform.childCount; i++)
        {
            var child = _currentmeter.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                allSlots.Add(child.gameObject);
                child.GetComponent<Slot>().slotID = allSlotIDCounter++;
            }
        }

        slotColumns.Clear();
        for (int i = 0; i < breadboardSlotIDCounter; i += 4)
        {
            var child = _breadboardUI.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                SlotColumn slotColumn = new SlotColumn(breadboardSlots[i].GetComponent<Slot>(),
                breadboardSlots[i + 1].GetComponent<Slot>(),
                breadboardSlots[i + 2].GetComponent<Slot>(),
                breadboardSlots[i + 3].GetComponent<Slot>());
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
        if (Input.GetMouseButtonDown(1))
        {
            RemoveComponent();
        }
        // // function attached to a button for testing
        // CalculateCircuit();
    }

    public void CalculateCircuit()
    {

        Debug.Log("Started Circuit Calculation");
        slotColumns.Clear();
        for (int i = 0; i < breadboardSlotIDCounter; i += 4)
        {
            var child = _breadboardUI.transform.GetChild(i);
            child.GetComponent<Slot>().componentAdded = false;
            if (child.CompareTag("BBSlot"))
            {
                SlotColumn slotColumn = new SlotColumn(
                    breadboardSlots[i].GetComponent<Slot>(),
                    breadboardSlots[i + 1].GetComponent<Slot>(),
                    breadboardSlots[i + 2].GetComponent<Slot>(),
                    breadboardSlots[i + 3].GetComponent<Slot>());

                slotColumns.Add(slotColumn);
                if (!(slotColumns.Last().isGroundSlot || slotColumns.Last().isPowerSlot))
                {
                    slotColumns.Last().ChangeAllVoltages(-1.0f);
                }
            }
        }

        UpdateSuccessors();
        do
        {
            RemoveDeadEnds();
        } while (foundDeadEnd);

        CalculateElectricalData();
        //  for (int i = 0; i < slotColumns.Count; i++)
        // {
        //     Debug.Log(slotColumns[i].printAllColumnConnections());
        // }

        Debug.Log("***** DONE CALCULATION *****");

        Debug.Log("Updating VoltMeter");
        _voltmeter.GetComponent<Voltmeter>().UpdateTerminals();
        Debug.Log("VOLTMETER UPDATED");

        Debug.Log("UPDATING AmMETER");
        _currentmeter.GetComponent<Currentmeter>().UpdateText(circuitCurrent);
    }

    private int GetSlotColumn(int slot)
    {
        int column = -1;

        column = (int)Math.Floor((double)(slot / 4.0));

        return column;
    }

    private void UpdateSuccessors()
    {
        Debug.Log("UPDATING");
        //Check if pos is connected to Ground or to VCC
        if (_currentmeter.GetComponent<Currentmeter>().posSlot.GetComponent<Slot>().itemPlaced != null &&
            _currentmeter.GetComponent<Currentmeter>().negSlot.GetComponent<Slot>().itemPlaced != null)
        {
            Debug.Log("NO NULLS");
            Slot amPosSlot = _currentmeter.GetComponent<Currentmeter>().posSlot.GetComponent<Slot>();
            Slot amNegSlot = _currentmeter.GetComponent<Currentmeter>().negSlot.GetComponent<Slot>();
            if (amPosSlot.slotPair.GetComponent<Slot>().slotType == Globals.SlotType.groundSlot)
            {
                Debug.Log("yes1");
                int updateGroundSlot = GetSlotColumn(amNegSlot.slotPair.GetComponent<Slot>().slotID);
                slotColumns[updateGroundSlot].connectedToGround = true;
            }
            if (amPosSlot.slotPair.GetComponent<Slot>().slotType == Globals.SlotType.startSlot)
            {
                Debug.Log("yes2");
                int updateStartSlot = GetSlotColumn(amNegSlot.slotPair.GetComponent<Slot>().slotID);
                slotColumns[updateStartSlot].connectedToPower = true;
            }
            if (amNegSlot.slotPair.GetComponent<Slot>().slotType == Globals.SlotType.groundSlot)
            {
                Debug.Log("yes3");
                int updateGroundSlot = GetSlotColumn(amPosSlot.slotPair.GetComponent<Slot>().slotID);
                slotColumns[updateGroundSlot].connectedToGround = true;
            }
            if (amNegSlot.slotPair.GetComponent<Slot>().slotType == Globals.SlotType.startSlot)
            {
                Debug.Log("yes4");
                int updateStartSlot = GetSlotColumn(amPosSlot.slotPair.GetComponent<Slot>().slotID);
                slotColumns[updateStartSlot].connectedToPower = true;
            }
        }

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
        amMeterStart = -1;
        amMeterEnd = -1;
        for (int i = 0; i < slotColumns.Count; i++)
        {
            SlotColumn currentSlot = slotColumns[i];
            if (currentSlot.columnID == 4 || currentSlot.columnID == 5)
            {
                if (slotColumns[i].connectedToAmMeter)
                {
                    if (amMeterStart == -1)
                    {
                        amMeterStart = slotColumns[i].columnID;
                        // Debug.Log("Start is: " + slotColumns[i].columnID);
                    }
                    else
                    {
                        amMeterEnd = slotColumns[i].columnID;
                        // Debug.Log("End is: " + slotColumns[i].columnID);
                    }
                }
                continue;
            }
            if (slotColumns[i].columnConnections.Count == 1 && !slotColumns[i].connectedToAmMeter)
            {
                Debug.Log("Deadend: " + slotColumns[i].columnID);
                slotColumns[i].columnConnections.Clear();
                RemovePairComponents(slotColumns[i]);
                RemoveConnectionSlot(slotColumns[i].columnID);
                slotColumns[i].isDeadEnd = true;
                slotColumns[i].connectedToGround = false;
                slotColumns[i].connectedToPower = false;
                slotColumns[i].ChangeAllVoltages(-1.0f);
                slotColumns[i].columnConnections = new List<int>();
                foundDeadEnd = true;
            }
            else if (slotColumns[i].connectedToAmMeter)
            {
                if (slotColumns[i].connectedToAmMeter)
                {

                    if (amMeterStart == -1)
                    {
                        amMeterStart = slotColumns[i].columnID;
                        // Debug.Log("Start is: " + slotColumns[i].columnID);
                    }
                    else
                    {
                        amMeterEnd = slotColumns[i].columnID;
                        // Debug.Log("End is: " + slotColumns[i].columnID);
                    }
                }
            }
        }
    }
    private void RemovePairComponents(SlotColumn column)
    {
        for (int i = 0; i < 4; i++)
        {
            if (column.slotList[i].itemPlaced != null)
            {
                if (CheckIfValidSlotType(column.slotList[i].slotPair.GetComponent<Slot>()))
                {
                    slotColumns[GetSlotColumn(column.slotList[i].slotPair.GetComponent<Slot>().slotID)].ignoreColumn = true;
                }
                    
            }
        }
    }


    private void RemoveConnectionSlot(int slotIDToRemove)
    {
        for (int i = 0; i < slotColumns.Count; i++)
        {
            if (slotColumns[i].columnConnections.Contains(slotIDToRemove))
            {
                int elementIndex = slotColumns[i].columnConnections.IndexOf(slotIDToRemove);
                // Debug.Log("Column: "+ slotColumns[i].columnID + " Removing connection: "+ slotIDToRemove);
                // slotColumns[i].columnConnections[elementIndex] = -1;
                slotColumns[i].columnConnections.Remove(slotIDToRemove);
                // slotColumns[i].columnConnections[slotColumns[i].columnConnections.FindIndex(ind => ind.Equals(slotIDToRemove))] = -1;
            }
        }
    }

    private void CalculateElectricalData()
    {
        float resistanceTotal = 0;
        List<int> colsToIgnore = new List<int>();
        Debug.Log("AM start: " + amMeterStart);
        Debug.Log("AM End: " + amMeterEnd);
        for (int i = 0; i < breadboardSlots.Count; i++)
        {
            breadboardSlots[i].GetComponent<Slot>().componentAdded = false;
        }
        for (int i = 0; i < slotColumns.Count; i++)
        {
            // slotColumns[i].printAllColumnConnections();
            // bool pairHasConnection = false;
            // Debug.Log("Checking column for connection" + slotColumns[i].columnID);
            // for(int j = 0 ; j < slotColumns[i].columnConnections.Count ; j++)
            // {
            //     Debug.Log("Checking connection" + slotColumns[i].columnConnections[j]);
            //     if(slotColumns[i].columnConnections[j] != -1 && slotColumns[i].columnConnections[j] != slotColumns[i].columnID)
            //     {
            //         Debug.Log("Has valid connection");
            //         pairHasConnection = true;
            //     }
            // }
            if (slotColumns[i].connectedToPower && slotColumns[i].connectedToGround && !slotColumns[i].ignoreColumn)
            {
                // Debug.Log("Checking res for column: " + slotColumns[i].columnID);
                float columnResistance = slotColumns[i].ResistorVal();
                resistanceTotal += columnResistance;
                if (slotColumns[i].columnID == amMeterStart)
                {
                    i = amMeterEnd;
                }
                // Debug.Log("DONE for column: "+ slotColumns[i].columnID + " Value of: " + columnResistance);
            }
        }
        // Debug.Log("Res Total: " + resistanceTotal);
        if (resistanceTotal > 0)
        {
            circuitCurrent = slotColumns[5].voltage / resistanceTotal;
        }
        else
        {
            circuitCurrent = 99999;
        }

        Debug.Log("Circuit Current: " + circuitCurrent);


        float prevVoltage = slotColumns[5].voltage;
        for (int i = 0; i < slotColumns.Count; i++)
        {
            if (slotColumns[i].connectedToPower && slotColumns[i].connectedToGround)
            {
                if (slotColumns[i].columnID == amMeterStart)
                {
                    i = amMeterEnd;
                }
                slotColumns[i].ChangeAllVoltages(prevVoltage);
                float voltageDrop = circuitCurrent * slotColumns[i].impedance;
                prevVoltage -= voltageDrop;
                // Debug.Log("voltage drop:" + voltageDrop);
            }
        }
    }


    public static void ItemClicked()
    {
        if (Globals.mouseClickAction == Globals.MouseClickAction.NoClick && spawnableItem != null)
        {
            // Debug.Log("item clicked");
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
        Globals.mouseClickAction = Globals.MouseClickAction.TwoClicks_FirstClick;
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
                        // Debug.Log("First Click GOOD");
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
                        // Debug.Log("Second Click GOOD DRAWING LINE");

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
                placeItem = Instantiate(_wireImage, _placedImages.transform);
                placeItem.GetComponent<Image>().color = Resources.Load<Image>(
                spawnableItem.itemName.ToString() +
                spawnableItem.itemValue.ToString()).color;
                break;

            case (Globals.AvailableItems.Resistor):
                placeItem = Instantiate(_resistorImage, _placedImages.transform);
                break;

            case (Globals.AvailableItems.Capacitor):
                placeItem = Instantiate(_capacitorImage, _placedImages.transform);
                break;

            default:
                placeItem = Instantiate(_wireImage, _placedImages.transform);
                break;
        }

        placeItem.gameObject.AddComponent<PlacedImages>();
        placeItem.GetComponent<PlacedImages>().itemID = spawnableItem.itemID;
        placeItem.GetComponent<PlacedImages>().slotA = _pointA.GetComponent<Slot>().slotID;
        placeItem.GetComponent<PlacedImages>().slotB = _pointB.GetComponent<Slot>().slotID;
        placeItem.tag = "PlacedItem";


        float distance = Vector2.Distance(_pointA.transform.position, _pointB.transform.position);
        // Debug.Log("Distance: " + distance);
        distance = distance * 0.85f; //Have it be slightly shorter so it gets the centers

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


    private void RemoveComponent()
    {
        GameObject itemToRemove;
        itemToRemove = CheckIfPlacedComponent();
        if (itemToRemove != null)
        {
            Debug.Log("Removing item between slots: " + itemToRemove.GetComponent<PlacedImages>().slotA + " and " + itemToRemove.GetComponent<PlacedImages>().slotB);
            allSlots[itemToRemove.GetComponent<PlacedImages>().slotA].GetComponent<Slot>().RemoveItem();
            allSlots[itemToRemove.GetComponent<PlacedImages>().slotB].GetComponent<Slot>().RemoveItem();
            Globals.inventoryItems[itemToRemove.GetComponent<PlacedImages>().itemID].isPlaced = false;
        }

        Destroy(itemToRemove);
    }


    private GameObject CheckIfPlacedComponent()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.transform.CompareTag("PlacedItem"))
                {
                    return results[i].gameObject;

                }
            }
        }
        return null;
    }


    private bool CheckIfValidSlotType(Slot slot)
    {
        if (slot.slotType == Globals.SlotType.defaultSlot ||
                slot.slotType == Globals.SlotType.groundSlot ||
                slot.slotType == Globals.SlotType.startSlot)
        {
            return true;
        }
        return false;
    }
}