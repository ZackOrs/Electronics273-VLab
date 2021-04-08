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
    public float voltage = 1.1f;
    public float current = 0.0056f;
    [SerializeField] GameObject focusPoint = null;

    [SerializeField] GameObject BananaSlotConnectionsPanel = null;
    [SerializeField] GameObject WireCreationManager = null;
    private GameObject clickedInputGameObject;

    [SerializeField] GameObject PowerLight = null;
    private Globals.PowerSupplyInput clickedInput;
    private bool changingConnection = false;
    private bool powerOn = false;


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
                WireCreationManager.GetComponent<WireCreationManager>().CreateWireStartPointEndPoint(clickedInputGameObject.transform, BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked);
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
                clickedInputGameObject = transform.Find(clickedButton).gameObject;
                break;

            case ("PointP"):
                //Nothing done with this banana plug in experiment 2
                Debug.Log("Banana P clicked");
                break;

            case ("CurrentSource"):
                clickedInput = Globals.PowerSupplyInput.currentSource;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                clickedInputGameObject = transform.Find(clickedButton).gameObject;
                break;

            case ("Ground"):
                clickedInput = Globals.PowerSupplyInput.ground;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                clickedInputGameObject = transform.Find(clickedButton).gameObject;
                break;

            case ("BtnPower"):
            PowerButton();
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
        if (powerOn)
        {
            voltage = (numberOfTurnsVoltage * 0.11125f) + 1.1f;
            Debug.Log("Voltage:" + ((numberOfTurnsVoltage * 0.11125f) + 1.1f) + " V");
        }
        else
        {
            voltage = 0;
            Debug.Log("No power, Voltage: " + voltage);
        }

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
        if (powerOn)
        {
            current = (numberOfTurnsCurrent * 0.000555f) + 0.0056f;
            Debug.Log("Current:" + ((numberOfTurnsCurrent * 0.000555f) + 0.0056f) + " A");
        }
        else
        {
            current = 0;
            Debug.Log("No power, current: " + current);
        }
    }


    private void PowerButton()
    {
        Debug.Log("hit power switch");
        powerOn = !powerOn;
        if(powerOn)
        {
            PowerLight.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            PowerLight.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        CalculateCurrent();
        CalculateVoltage();
    }

}
