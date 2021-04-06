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
    [SerializeField] GameObject focusPoint = null;
    [SerializeField] GameObject BananaSlotConnectionsPanel = null;
    private Globals.AgilentInput clickedInput;

    private bool changingConnection = false;

    public float voltageReading = 0;

    public bool valueUpdated = false;

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
            displayValueText.text = voltageReading.ToString("0.000") + " V";
            valueUpdated = false;
        }

    }

    public void ButtonClickHandler(string clickedButton)
    {
        switch (clickedButton)
        {
            case ("BtnDCV"):
                //Button23Pressed();
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

            default:
                Debug.Log("No buttono");
                break;
        }
    }
}