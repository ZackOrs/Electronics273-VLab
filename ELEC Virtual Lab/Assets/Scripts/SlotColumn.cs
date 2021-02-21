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
    public float impedance;

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
                Slot slotpair = slot.slotPair.GetComponent<Slot>();
                if (slotpair.slotType == Globals.SlotType.defaultSlot ||
                     slotpair.slotType == Globals.SlotType.groundSlot ||
                      slotpair.slotType == Globals.SlotType.startSlot)
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
        float impedanceValue = 0;
        List<float> parallelComponents = new List<float>();
        for (int i = 0; i < slotList.Count; i++)
        {
            Slot slot = slotList[i];
            if (slot.itemPlaced != null)
            {
                if (slot.itemPlaced.itemName != Globals.AvailableItems.Wire && !slot.componentAdded)
                {
                    impedanceValue = GetImpedanceValue(slot);
                    parallelComponents = CheckIfInParallel(slot, i);

                    slot.componentAdded = true;
                    slot.slotPair.GetComponent<Slot>().componentAdded = true;
                }
            }
        }

        if (parallelComponents.Count > 1)
        {
            impedanceValue = CalculateParallelImpedance(parallelComponents);
        }

        Debug.Log("Column : " + columnID + " Resistance : " + impedanceValue);
        impedance = impedanceValue;
        return impedanceValue;
    }

    private float CalculateCapacitorImpedance()
    {
        float impedance = 0;


        return impedance;

    }

    private float CalculateParallelImpedance(List<float> parallelComponents)
    {
        float resistTot = 0;
        Debug.Log("Found " + parallelComponents.Count + " parallel resistors in column" + columnID);
        for (int i = 0; i < parallelComponents.Count; i++)
        {
            Debug.Log(parallelComponents[i].ToString());
            resistTot += (1 / parallelComponents[i]);
        }
        resistTot = 1 / resistTot;

        return resistTot;
    }

    private List<float> CheckIfInParallel(Slot currentSlot, int currentSlotNumber)
    {
        List<float> parallelResistors = new List<float>();

        parallelResistors.Add(GetImpedanceValue(currentSlot));
        int slotsEndSlot = getSlotColumn(currentSlot.slotPair.GetComponent<Slot>().slotID);
        for (int i = currentSlotNumber + 1; i < slotList.Count; i++)
        {

            Slot slot = slotList[i];
            if (slot.itemPlaced != null)
            {
                if (slot.itemPlaced.itemName != Globals.AvailableItems.Wire)
                {
                    int otherSlotEndSlot = getSlotColumn(slot.slotPair.GetComponent<Slot>().slotID);
                    if (slotsEndSlot == otherSlotEndSlot)
                    {
                        parallelResistors.Add(GetImpedanceValue(slot));
                        slot.componentAdded = true;
                        slot.slotPair.GetComponent<Slot>().componentAdded = true;
                    }
                }
            }
        }
        return parallelResistors;
    }


    private float GetImpedanceValue(Slot slot)
    {
        float impedanceVal = 0;
        if (slot.itemPlaced.itemName == Globals.AvailableItems.Resistor)
        {
            switch (slot.itemPlaced.itemValue)
            {
                case (0):
                    impedanceVal = 210;
                    break;

                case (1):
                    impedanceVal = 370;
                    break;

                case (2):
                    impedanceVal = 480;
                    break;

                default:
                    break;
            }
        }
        else if (slot.itemPlaced.itemName == Globals.AvailableItems.Capacitor)
        {
            float capacitorVal = 0;

            switch (slot.itemPlaced.itemValue)
            {
                case (0):
                    capacitorVal = 0.0000001f;
                    break;

                case (1):
                    capacitorVal = 0.000001f;
                    break;

                case (2):
                    capacitorVal = 0.000004f;
                    break;
                case (3):
                    capacitorVal = 0.000005f;
                    break;

                case (4):
                    capacitorVal = 0.000009f;
                    break;

                case (5):
                    capacitorVal = 0.000012f;
                    break;
                case (6):
                    capacitorVal = 0.000015f;
                    break;

                case (7):
                    capacitorVal = 0.000019f;
                    break;

                case (8):
                    capacitorVal = 0.000020f;
                    break;
                default:
                    break;
            }
            Debug.Log("Capacitor Val: " + capacitorVal);
            impedanceVal = 1 / ((2 * (float)Math.PI * 60) * capacitorVal);
            Debug.Log("Impedance value: " + impedanceVal);
        }


        return impedanceVal;
    }

}
