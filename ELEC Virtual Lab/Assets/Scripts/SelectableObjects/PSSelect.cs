using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PSSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";
    private bool _rotate;
    private int numberOfTurnsVoltage = 0;
    private int numberOfTurnsCurrent = 0;
    public float voltage = 0;
    public float current = 0;
    [SerializeField] GameObject focusPoint = null;

    [SerializeField] GameObject BananaSlotConnectionsPanel = null;
    private Globals.PowerSupplyInput clickedInput;
    private bool changingConnection = false;

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
        Camera.main.GetComponent<AnimateCamera>().targetObject = focusPoint;
    }

    void Update()
    {
        if (changingConnection)
        {
            if (BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().OptionClicked)
            {
                Globals.PSConnections[clickedInput] = BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked;
                Debug.Log("Updating Key: PSConnectios[" + clickedInput + "] To: " + BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked);
                BananaSlotConnectionsPanel.SetActive(false);
                BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().OptionClicked = false;
                changingConnection = false;
            }
        }
    }


    public void ButtonClickHandler(string clickedButton)
    {
        switch (clickedButton)
        {
            case ("VoltageSource"):
                clickedInput = Globals.PowerSupplyInput.voltageSource;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("PointP"):
                //Nothing done with this banana plug in experiment 2
                Debug.Log("Banana P clicked");
                break;

            case ("CurrentSource"):
                clickedInput = Globals.PowerSupplyInput.currentSource;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("Ground"):
                clickedInput = Globals.PowerSupplyInput.ground;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
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
