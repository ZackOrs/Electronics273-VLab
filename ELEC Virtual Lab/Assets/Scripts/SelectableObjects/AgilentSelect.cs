using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AgilentSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";

    //public GameObject Camera;
    [SerializeField] TMP_Text displayValueText = null;
    [SerializeField] TMP_Text displayModeText = null;
    [SerializeField] GameObject focusPoint = null;
    [SerializeField] GameObject BananaSlotConnectionsPanel = null;
    private Globals.AgilentInput clickedInput;

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
            return "Agilent";
        }
    }

    public override void onInteract()
    {
        Globals.currentMachine = "Agilent";
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
                Globals.AgilentConnections[clickedInput] = BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked;
                Debug.Log("Updating Key: AgilentConnections[" + clickedInput + "] To: " + BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked);
                BananaSlotConnectionsPanel.SetActive(false);
                BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().OptionClicked = false;
                changingConnection = false;
            }
        }

        if (valueUpdated)
        {
            if (Globals.AgilentConnections[Globals.AgilentInput.groundInput].Equals(Globals.BananaPlugs.noConnection) ||
            (Globals.AgilentConnections[Globals.AgilentInput.voltageInput].Equals(Globals.BananaPlugs.noConnection) && Globals.AgilentConnections[Globals.AgilentInput.currentInput].Equals(Globals.BananaPlugs.noConnection)))
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
                if (Globals.AgilentConnections[Globals.AgilentInput.currentInput].Equals(Globals.BananaPlugs.noConnection))
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
                Debug.Log("BTNDCV");
                DCVButtonClicked();
                break;

            case ("BtnDCI"):
                Debug.Log("BTNDCI");
                DCIButtonClicked();
                break;

            case ("Torus.006"):
                clickedInput = Globals.AgilentInput.voltageInput;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("Torus.009"):
                clickedInput = Globals.AgilentInput.groundInput;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("Torus.010"):
                clickedInput = Globals.AgilentInput.currentInput;
                BananaSlotConnectionsPanel.SetActive(true);
                changingConnection = true;
                break;

            case ("BtnAuto"):
                Debug.Log("Clicked Auto");
                break;

            case ("BtnRange"):
                Debug.Log("Clicked Range");
                break;

            case ("BtnFreq"):
                Debug.Log("Clicked Freq");
                break;

            case ("BtnOhms"):
                Debug.Log("Clicked Ohms");
                break;

            case ("BtnTemp"):
                Debug.Log("Clicked Temp");
                break;

            case ("BtnCapacitance"):
                Debug.Log("Clicked Capacitance");
                break;

            case ("BtnPower"):
                PowerButton();
                break;

            default:
                Debug.Log("No buttono");
                break;
        }
    }
    private void DCVButtonClicked()
    {
        meterMode = true;
        valueUpdated = true;
        displayModeText.text = "Reading: DC Voltage";
    }

    private void DCIButtonClicked()
    {
        meterMode = false;
        valueUpdated = true;
        displayModeText.text = "Reading: DC Current";
    }

    private void PowerButton()
    {
        powerOn = !powerOn;
        displayValueText.gameObject.SetActive(powerOn);
        displayModeText.gameObject.SetActive(powerOn);
        valueUpdated = true;

