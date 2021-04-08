using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FlukeSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";

    //public GameObject Camera;
    [SerializeField] TMP_Text displayValueText = null;
    [SerializeField] TMP_Text displayModeText = null;
    [SerializeField] GameObject focusPoint = null;
    [SerializeField] GameObject BananaSlotConnectionsPanel = null;

    private Globals.FlukeInput clickedInput;

    private bool changingConnection = false;

    public float voltageReading = 0;
    public float currentReading = 0;

    public bool valueUpdated = false;

    private bool meterMode = true; //True = Voltmeter, False = currentmeter

    private bool powerOn = false; //Initially off

    private int randomizeEveryXFramesCounter = 0;

    private float randomValue = 1;

    public override string Name
    {
        get
        {
            return "Fluke";
        }
    }

    public override void onInteract()
    {
        Globals.currentMachine = "Fluke";
        Globals.lookingAtFocusableObject = true;
        Camera.main.GetComponent<AnimateCamera>().targetObject = focusPoint;
    }

    void Update()
    {
        if (randomizeEveryXFramesCounter > Globals.RandomEveryXFrame)
        {
            randomValue = UnityEngine.Random.Range(0.97f, 1.03f);
            randomizeEveryXFramesCounter = 0;
        }

        if (changingConnection)
        {
            if (BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().OptionClicked)
            {
                Globals.FlukeConnections[clickedInput] = BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked;
                Debug.Log("Updating Key: FlukeConnections[" + clickedInput + "] To: " + BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked);
                BananaSlotConnectionsPanel.SetActive(false);
                BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().OptionClicked = false;
                changingConnection = false;
            }
        }

        if (valueUpdated)
        {
            if (Globals.FlukeConnections[Globals.FlukeInput.groundInput].Equals(Globals.BananaPlugs.noConnection) ||
            (Globals.FlukeConnections[Globals.FlukeInput.voltageInput].Equals(Globals.BananaPlugs.noConnection) && Globals.FlukeConnections[Globals.FlukeInput.currentInput].Equals(Globals.BananaPlugs.noConnection)))
            {
                displayValueText.text = "-.---";
                valueUpdated = false;
            }
            else if (meterMode)
            {
                voltageReading = voltageReading * randomValue;
                displayValueText.text = voltageReading.ToString("0.000") + " V";
                valueUpdated = false;
            }
            else
            {
                if (Globals.FlukeConnections[Globals.FlukeInput.currentInput].Equals(Globals.BananaPlugs.noConnection))
                {
                    displayValueText.text = "-.---";
                }
                else
                {
                    currentReading = currentReading * randomValue;
                    currentReading = currentReading < -99 ? -99 : currentReading;

                    displayValueText.text = (currentReading * -1000).ToString("0000.00") + " mA";
                }
                valueUpdated = false;
            }
        }
        randomizeEveryXFramesCounter++;
    }

    public void ButtonClickHandler(string clickedButton)
    {
        switch (clickedButton)
        {
            case ("BtnDCV"):
                //Button23Pressed();
                break;
            case ("VoltageInput"):
                clickedInput = Globals.FlukeInput.voltageInput;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("CommonGround"):
                clickedInput = Globals.FlukeInput.groundInput;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("CurrentInputmA"):
                clickedInput = Globals.FlukeInput.currentInput;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("BtnPower"):
                Debug.Log("Clicked Power");
                PowerButton();
                break;

            case ("BtnVolt"):
                VoltageButtonClick();
                break;

            case ("BtnCurrent"):
                CurrentButtonClick();
                break;

            default:
                Debug.Log("No buttono");
                break;
        }
    }
    private void VoltageButtonClick()
    {
        meterMode = true;
        valueUpdated = true;
        displayModeText.text = "Reading: DC Voltage";
    }

    private void CurrentButtonClick()
    {
        meterMode = false;
        valueUpdated = true;
        displayModeText.text = "Reading: DC Current";
    }

    private void PowerButton()
    {
        powerOn = !powerOn;
        valueUpdated = true;
        displayValueText.gameObject.SetActive(powerOn);
        displayModeText.gameObject.SetActive(powerOn);
    }
}