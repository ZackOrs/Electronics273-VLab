using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AgilentSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";

    //public GameObject Camera;
    [SerializeField] GameObject focusPoint = null;
    [SerializeField] string bananaPlugOption = "BananaPlugOption";
    [SerializeField] GameObject BananaSlotConnectionsPanel = null;
    private Globals.AgilentInput clickedInput;

    public float VoltageReading = 0;

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
        if (BananaSlotConnectionsPanel.activeSelf)
        {
            if (BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().OptionClicked)
            {
                Globals.AgilentConnections.Remove(clickedInput);
                Globals.AgilentConnections.Add(clickedInput, BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked);
                Debug.Log("Adding Connection" + clickedInput + " and " + BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().BananaPlugsSlotClicked);
                BananaSlotConnectionsPanel.SetActive(false);
                BananaSlotConnectionsPanel.GetComponent<BreadboardBananaConnectionPanelButtons>().OptionClicked = false;
            }
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
                Debug.Log("Clicked Pos");
                clickedInput = Globals.AgilentInput.voltageInput;
                BananaSlotConnectionsPanel.SetActive(true);
                // Globals.AgilentConnections.Remove(Globals.AgilentInput.voltageInput);
                // Globals.AgilentConnections.Add(Globals.AgilentInput.voltageInput,Globals.BananaPlugs.B1);
                break;

            case ("Torus.009"):
                Debug.Log("Clicked Neg");
                clickedInput = Globals.AgilentInput.groundInput;
                BananaSlotConnectionsPanel.SetActive(true);
                break;
            default:
                Debug.Log("No buttono");
                break;

        }
    }

    private void Button23Pressed()
    {

    }
}