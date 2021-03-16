using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

public class ItemClickHandler : MonoBehaviour
{

    public static SpawnableItem spawnableItem;

    public static GameObject buttonClicked;

    [SerializeField] GameObject _breadboardUI = null;
    [SerializeField] GameObject _voltmeter = null;
    [SerializeField] GameObject _currentmeter = null;
    [SerializeField] GameObject _powerSupply = null;

    [SerializeField] Image _wireImage = null;
    [SerializeField] Image _resistorImage = null;
    [SerializeField] Image _capacitorImage = null;

    [SerializeField] GameObject _placedImages = null;

    public static bool isBBSlotFree = false;
    private GameObject _pointA = null;
    private GameObject _pointB = null;
    public static List<GameObject> breadboardSlots = new List<GameObject>();
    public static List<GameObject> allSlots = new List<GameObject>();

    int breadboardSlotIDCounter = 0;
    int allSlotIDCounter = 0;

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

    public void SpiceSharpCalculation()
    {
        ClearAllSlots();

        var ckt = new Circuit();
        
        for (int i = 0; i < breadboardSlotIDCounter; i++)
        {
            if (breadboardSlots[i].GetComponent<Slot>().itemPlaced != null && !breadboardSlots[i].GetComponent<Slot>().slotChecked)
            {
                if (breadboardSlots[i].GetComponent<Slot>().slotPair.GetComponent<Slot>().slotType == Globals.SlotType.defaultSlot)
                {
                    AddElectricalElement(breadboardSlots[i].GetComponent<Slot>(), ckt);
                    breadboardSlots[i].GetComponent<Slot>().slotChecked = true;
                    breadboardSlots[i].GetComponent<Slot>().slotPair.GetComponent<Slot>().slotChecked = true;
                }
            }
        }

        VoltageSource powerSupply = AddVoltageSource(ckt);
        ckt.Add(powerSupply);

        Debug.Log("Starting calculation ");

        // Create a DC sweep and register to the event for exporting simulation data

        var dc = new DC("dc", "PS", 5, 5, 1);
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
            Debug.Log("Real voltage " + (new RealVoltageExport(dc, "C0", "C1")).Value);
        };
        // Run the simulation
        dc.Run(ckt);
    }

    private void ClearAllSlots()
    {
        for (int i = 0; i < breadboardSlotIDCounter; i++)
        {
            var child = _breadboardUI.transform.GetChild(i);

            if (child.CompareTag("BBSlot"))
            {
                child.GetComponent<Slot>().slotChecked = false;
            }
        }

        for (int i = 0; i < _voltmeter.transform.childCount; i++)
        {
            var child = _voltmeter.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                child.GetComponent<Slot>().slotChecked = false;
            }
        }

        for (int i = 0; i < _currentmeter.transform.childCount; i++)
        {
            var child = _currentmeter.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                child.GetComponent<Slot>().slotChecked = false;
            }
        }

        // for (int i = 0; i < _powerSupply.transform.childCount; i++)
        // {
        //     var child = _powerSupply.transform.GetChild(i);
        //     if (child.CompareTag("BBSlot"))
        //     {
        //        child.GetComponent<Slot>().slotChecked = false;
        //     }
        // }

    }

    private VoltageSource AddVoltageSource(Circuit ckt)
    {
        VoltageSource source = new VoltageSource("PS", "POS", "0", 5);


        for (int i = 0; i < _voltmeter.transform.childCount; i++)
        {
            var child = _voltmeter.transform.GetChild(i);
            if (child.CompareTag("BBSlot"))
            {
                if (child.GetComponent<Slot>().itemPlaced != null && !child.GetComponent<Slot>().slotChecked)
                {
                    AttachPowerSupply(child, ckt);
                }
            }
        }

        return source;
    }

    private void AttachPowerSupply(Transform slot, Circuit ckt)
    {

        if (slot.name.Equals("VM Pos Slot"))
        {
            if (slot.GetComponent<Slot>().itemPlaced != null && !slot.GetComponent<Slot>().slotChecked)
            {
                AddElectricalElement(slot.GetComponent<Slot>(), ckt, "POS",
                    "C" + GetSlotColumn(slot.GetComponent<Slot>().slotPair.GetComponent<Slot>().slotID).ToString());
                slot.GetComponent<Slot>().slotChecked = true;
                slot.GetComponent<Slot>().slotPair.GetComponent<Slot>().slotChecked = true;
            }
        }
        if (slot.name.Equals("VM Neg Slot"))
        {
            if (slot.GetComponent<Slot>().itemPlaced != null && !slot.GetComponent<Slot>().slotChecked)
            {
                AddElectricalElement(slot.GetComponent<Slot>(), ckt, "C" + GetSlotColumn(slot.GetComponent<Slot>().slotPair.GetComponent<Slot>().slotID).ToString(),
                    "0");
                slot.GetComponent<Slot>().slotChecked = true;
                slot.GetComponent<Slot>().slotPair.GetComponent<Slot>().slotChecked = true;
            }
        }

    }
    private void AddElectricalElement(Slot slot, Circuit ckt)
    {
        string componentName = "S" + slot.slotID;
        string componentStart = "C" + GetSlotColumn(slot.slotID);
        string componentEnd = "C" + GetSlotColumn(slot.slotPair.GetComponent<Slot>().slotID);
        float value = GetComponentValue(slot);

        switch (slot.itemPlaced.itemName)
        {
            case (Globals.AvailableItems.Wire):
                Resistor wire = new Resistor(componentName, componentStart, componentEnd, 0);
                Debug.Log("Adding Wire: " + componentName + " " + componentStart + " " + componentEnd + " " + 0);
                ckt.Add(wire);
                break;

            case (Globals.AvailableItems.Resistor):
                Resistor resistor = new Resistor(componentName, componentStart, componentEnd, value);
                Debug.Log("Adding Resistor: " + componentName + " " + componentStart + " " + componentEnd + " " + value);
                ckt.Add(resistor);
                break;

            case (Globals.AvailableItems.Capacitor):
                Capacitor capacitor = new Capacitor(componentName, componentStart, componentEnd, value);
                Debug.Log("Adding Capacitor: " + componentName + " " + componentStart + " " + componentEnd + " " + value);
                ckt.Add(capacitor);
                break;

            default:
                break;
        }

    }

    private void AddElectricalElement(Slot slot, Circuit ckt, string componentStart, string componentEnd)
    {
        string componentName = "S" + slot.slotID;
        float value = GetComponentValue(slot);

        switch (slot.itemPlaced.itemName)
        {
            case (Globals.AvailableItems.Wire):
                Resistor wire = new Resistor(componentName, componentStart, componentEnd, 0);
                Debug.Log("Adding Wire: " + componentName + " " + componentStart + " " + componentEnd + " " + 0);
                ckt.Add(wire);
                break;

            case (Globals.AvailableItems.Resistor):
                Resistor resistor = new Resistor(componentName, componentStart, componentEnd, value);
                Debug.Log("Adding Resistor: " + componentName + " " + componentStart + " " + componentEnd + " " + value);
                ckt.Add(resistor);
                break;

            case (Globals.AvailableItems.Capacitor):
                Capacitor capacitor = new Capacitor(componentName, componentStart, componentEnd, value);
                Debug.Log("Adding Capacitor: " + componentName + " " + componentStart + " " + componentEnd + " " + value);
                ckt.Add(capacitor);
                break;

            default:
                break;
        }

    }

    private float GetComponentValue(Slot slot)
    {
        float componentVal = 0;
        if (slot.itemPlaced.itemName == Globals.AvailableItems.Resistor)
        {
            switch (slot.itemPlaced.itemValue)
            {
                case (0):
                    componentVal = 210;
                    break;

                case (1):
                    componentVal = 370;
                    break;

                case (2):
                    componentVal = 480;
                    break;

                default:
                    break;
            }
        }
        else if (slot.itemPlaced.itemName == Globals.AvailableItems.Capacitor)
        {
            switch (slot.itemPlaced.itemValue)
            {
                case (0):
                    componentVal = 0.0000001f;
                    break;

                case (1):
                    componentVal = 0.000001f;
                    break;

                case (2):
                    componentVal = 0.000004f;
                    break;
                case (3):
                    componentVal = 0.000005f;
                    break;

                case (4):
                    componentVal = 0.000009f;
                    break;

                case (5):
                    componentVal = 0.000012f;
                    break;
                case (6):
                    componentVal = 0.000015f;
                    break;

                case (7):
                    componentVal = 0.000019f;
                    break;

                case (8):
                    componentVal = 0.000020f;
                    break;
                default:
                    break;
            }
            // componentVal = 1 / ((2 * (float)Math.PI * 60) * componentVal);
        }
        return componentVal;
    }

    private int GetSlotColumn(int slot)
    {
        int column = -1;

        column = (int)Math.Floor((double)(slot / 4.0));

        return column;
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
}