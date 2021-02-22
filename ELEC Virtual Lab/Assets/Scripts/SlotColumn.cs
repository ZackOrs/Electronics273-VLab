using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotColumn
{

    public int columnID;

    public Slot slot1;
    public Slot slot2;
    public Slot slot3;
    public Slot slot4;

    public bool connectedToGround = false;
    public bool connectedToPower = false;

    public bool isPowerSlot = false;
    public bool isGroundSlot = false;

    public bool isDeadEnd = false;

    public float voltage;
    public float resistance;

    List<int> slotPairConnection = new List<int>();
    public List<int> columnConnections = new List<int>();
    List<int> slotConnection = new List<int>();



    List<Slot> slotList = new List<Slot>();

    public SlotColumn(Slot _slot1, Slot _slot2, Slot _slot3, Slot _slot4)
    {
        columnID = _slot1.slotID / 4;
        slot1 = _slot1;
        slot2 = _slot2;
        slot3 = _slot3;
        slot4 = _slot4;

        voltage = slot1.voltage;
        slotList.Add(slot1);
        slotList.Add(slot2);
        slotList.Add(slot3);
        slotList.Add(slot4);

        if (columnID == 4)
        {
            isGroundSlot = true;
        }
        if (columnID == 5)
        {
            isPowerSlot = true;
        }
        
        foreach (Slot slot in slotList)
        {
            if (slot.slotPair != null)
            {
                //Prevent connections to self
                if (getSlotColumn(slot.slotPair.GetComponent<Slot>().slotID) != columnID)
                {
                    slotConnection.Add(slot.slotID);
                    slotPairConnection.Add(slot.slotPair.GetComponent<Slot>().slotID);
                    columnConnections.Add(getSlotColumn(slotPairConnection.Last()));
                    if (columnConnections.Last() == 4)
                    {
                        connectedToGround = true;
                    }
                    if (columnConnections.Last() == 5)
                    {
                        connectedToPower = true;
                    }
                }

            }
        }
    }

    public string PrintAllSlotConnections()
    {
        string allPairs = "Column: " + columnID + " has " + slotConnection.Count.ToString() + " connections";

        // for (int i = 0; i < slotConnection.Count; i++)
        // {
        //     allPairs += ("Slot: " + slotConnection[i].ToString() + " connects to Slot: " + slotPairConnection[i].ToString() + "\t");
        // }

        return allPairs;
    }

    public string printAllColumnConnections()
    {
        string allColumns = "Column " + columnID + ": ";
        for (int i = 0; i < columnConnections.Count; i++)
        {
            allColumns += (columnConnections[i] + ", ");
        }

        return allColumns;
    }

    public void ChangeAllVoltages(float newVoltage)
    {
        if (!isGroundSlot)
        {
            foreach (Slot slot in slotList)
            {
                slot.voltage = newVoltage;
            }
        }

    }

    public int getSlotColumn(int slotToFind)
    {
        int column = slotToFind;
        column = (int)Math.Floor((decimal)(column / 4));

        return column;
    }



    public float ResistorVal()
    {
        float resistorVal = 0;
        List<float> parallelResistors = new List<float>();
        for (int i = 0; i < slotList.Count; i++)
        {
            Slot slot = slotList[i];
            if (slot.itemPlaced != null)
            {
                Debug.Log("Column " + columnID + "Resistor added?" + slot.resistorAdded);
                if (slot.itemPlaced.itemName == Globals.AvailableItems.Resistor && !slot.resistorAdded)
                {
                    Debug.Log("Column" + columnID + "Adding resistor: " + GetResistorResistorValue(slot));
                    resistorVal = GetResistorResistorValue(slot);

                    parallelResistors = CheckIfInParallel(slot, i);

                    slot.resistorAdded = true;
                    slot.slotPair.GetComponent<Slot>().resistorAdded = true;
                }
            }
        }

        if (parallelResistors.Count > 1)
        {
            resistorVal = CalculateParallelResistance(parallelResistors);
        }

        Debug.Log("Column : " + columnID + " Resistance : " + resistorVal);
        resistance = resistorVal;
        return resistorVal;
    }

    private float CalculateParallelResistance(List<float> parallelResistors)
    {
        float resistTot = 0;
        Debug.Log("Found "+ parallelResistors.Count + " parallel resistors in column" + columnID);
        for (int i = 0; i < parallelResistors.Count; i++)
        {
            Debug.Log(parallelResistors[i].ToString());
            resistTot += (1 / parallelResistors[i]);
        }
        resistTot = 1 / resistTot;

        return resistTot;
    }

    private List<float> CheckIfInParallel(Slot currentSlot, int currentSlotNumber)
    {
        List<float> parallelResistors = new List<float>();

        parallelResistors.Add(GetResistorResistorValue(currentSlot));

        int slotsEndSlot = getSlotColumn(currentSlot.slotPair.GetComponent<Slot>().slotID);
        for (int i = currentSlotNumber + 1; i < slotList.Count; i++)
        {

            Slot slot = slotList[i];
            if (slot.itemPlaced != null)
            {
                if (slot.itemPlaced.itemName == Globals.AvailableItems.Resistor)
                {
                    Debug.Log("Resistors in parallel");
                    int otherSlotEndSlot = getSlotColumn(slot.slotPair.GetComponent<Slot>().slotID);
                    if (slotsEndSlot == otherSlotEndSlot)
                    {
                        parallelResistors.Add(GetResistorResistorValue(slot));
                        slot.resistorAdded = true;
                        slot.slotPair.GetComponent<Slot>().resistorAdded = true;
                    }
                }
            }
        }
        return parallelResistors;
    }


    private float GetResistorResistorValue(Slot slot)
    {
        float impedanceResistorVal = 0;
        switch (slot.itemPlaced.itemValue)
        {
            case (0):
                impedanceResistorVal = 210;
                break;

            case (1):
                impedanceResistorVal = 370;
                break;

            case (2):
                impedanceResistorVal = 480;
                break;

            default:
                break;
        }

        

        return impedanceResistorVal;
    }

}
