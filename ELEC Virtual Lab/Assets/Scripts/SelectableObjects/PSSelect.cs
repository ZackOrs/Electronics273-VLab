using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PSSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";

    public GameObject Camera;
    private bool _rotate;
    private int numberOfTurnsVoltage = 0;
    private int numberOfTurnsCurrent = 0;
    public float voltage = 0;
    public float current = 0;
    [SerializeField] GameObject focusPoint = null;

    public override string Name
    {
        get
        {
            return "PS";
        }
    }



    public override void onInteract()
    {
        Globals.currentMachine = "PowerSupply";
        Globals.lookingAtFocusableObject = true;
        Camera.GetComponent<AnimateCamera>().targetObject = focusPoint;
    }

    void Update()
    {

    }


    public void ButtonClickHandler(string clickedButton)
    {
        switch (clickedButton)
        {
            case ("VoltageSource"):
                Debug.Log("Banana Voltage clicked");
                Globals.PSConnections.Remove(Globals.PowerSupplyInput.voltageSource);
                Globals.PSConnections.Add(Globals.PowerSupplyInput.voltageSource, Globals.BananaPlugs.B1);
                break;

            case ("PointP"):
                //Nothing done with this banana plug in experiment 2
                Debug.Log("Banana P clicked");
                break;

            case ("CurrentSource"):
                Debug.Log("Banana Current clicked");
                Globals.PSConnections.Remove(Globals.PowerSupplyInput.currentSource);
                Globals.PSConnections.Add(Globals.PowerSupplyInput.currentSource, Globals.BananaPlugs.B3);
                break;

            case ("Ground"):
                Debug.Log("Banana Ground clicked");
                Globals.PSConnections.Remove(Globals.PowerSupplyInput.ground);
                Globals.PSConnections.Add(Globals.PowerSupplyInput.ground, Globals.BananaPlugs.B0);
                break;

            case ("VoltageKnobPos"):
                IncreaseVoltage(transform.Find("VoltageKnob").gameObject);
                break;

            case ("VoltageKnobNeg"):
                DecreaseVoltage(transform.Find("VoltageKnob").gameObject);
                break;

            case ("CurrentKnobPos"):
                IncreaseCurrent(transform.Find("CurrentKnob").gameObject);
                break;

            case ("CurrentKnobNeg"):
                DecreaseCurrent(transform.Find("CurrentKnob").gameObject);
                break;

            default:
                Debug.Log("No buttono");
                break;

        }
    }

    public void IncreaseVoltage(GameObject voltageKnob)
    {
        if (numberOfTurnsVoltage < 80)
        {
            Vector3 currRot = new Vector3(0, 0, 45.0f);
            voltageKnob.transform.Rotate(currRot);
            numberOfTurnsVoltage++;
            CalculateVoltage();
        }

        else
        {
            Debug.Log("Cannot increase anymore");
        }

    }

    public void DecreaseVoltage(GameObject voltageKnob)
    {
        if (numberOfTurnsVoltage > 0)
        {
            Vector3 currRot = new Vector3(0, 0, -45.0f);
            voltageKnob.transform.Rotate(currRot);
            numberOfTurnsVoltage--;
            CalculateVoltage();
        }
        else
        {
            Debug.Log("Cannot decrease anymore");
        }
    }

    private void CalculateVoltage()
    {
        voltage = (numberOfTurnsVoltage * 0.11125f) + 1.1f;
        Debug.Log("Voltage:" + ((numberOfTurnsVoltage * 0.11125f) + 1.1f) + " V");
    }

    public void IncreaseCurrent(GameObject currentKnob)
    {
        if (numberOfTurnsCurrent < 80)
        {
            Vector3 currRot = new Vector3(0, 0, 45.0f);
            currentKnob.transform.Rotate(currRot);
            numberOfTurnsCurrent++;
            CalculateCurrent();
        }

        else
        {
            Debug.Log("Cannot increase anymore");
            CalculateCurrent();
        }

    }

    public void DecreaseCurrent(GameObject currentKnob)
    {
        if (numberOfTurnsCurrent > 0)
        {
            Vector3 currRot = new Vector3(0, 0, -45.0f);
            currentKnob.transform.Rotate(currRot);
            numberOfTurnsCurrent--;
            CalculateCurrent();
        }
        else
        {
            Debug.Log("Cannot decrease anymore");
        }
    }

    private void CalculateCurrent()
    {
        current = (numberOfTurnsCurrent * 0.000555f) + 0.0056f;
        Debug.Log("Current:" + ((numberOfTurnsCurrent * 0.000555f) + 0.0056f) + " A");
    }

}
