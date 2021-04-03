using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AgilentSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";

    public GameObject Camera;
    [SerializeField] GameObject focusPoint = null;

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
        Camera.GetComponent<AnimateCamera>().targetObject = focusPoint;
        
    }

    void Update()
    {

    }

    public void ButtonClickHandler(string clickedButton)
    {
        switch(clickedButton){
            case("BtnDCV"):
            Button23Pressed();
            break;
            case("Torus.004"):
            Debug.Log("Clicked Pos");
            Globals.AgilentConnections.Remove(Globals.AgilentInput.voltageInput);
            Globals.AgilentConnections.Add(Globals.AgilentInput.voltageInput,Globals.BananaPlugs.B1);
            break;

            case("Torus.009"):
            Debug.Log("Clicked Neg");
            Globals.AgilentConnections.Remove(Globals.AgilentInput.groundInput);
            Globals.AgilentConnections.Add(Globals.AgilentInput.groundInput,Globals.BananaPlugs.B0);
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