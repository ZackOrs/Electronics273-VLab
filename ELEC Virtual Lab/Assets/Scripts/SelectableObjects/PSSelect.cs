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
        if(_rotate)
        {
            transform.Rotate(new Vector3(0,10,0));
        }
    }


    public void ButtonClickHandler(string clickedButton)
    {
        switch(clickedButton){
            case("PointE"):
            Debug.Log("Banana E clicked");
            Globals.PSConnections.Add(Globals.PowerSupplyInput.voltageInput,Globals.BananaPlugs.B1);
            break;

            case("PointP"):
            //Nothing done with this banana plug in experiment 2
            Debug.Log("Banana P clicked");
            break;

            case("PointC"):
            Debug.Log("Banana C clicked");
            Globals.PSConnections.Add(Globals.PowerSupplyInput.currentInput,Globals.BananaPlugs.B3);
            break;

            case("Ground"):
            Debug.Log("Banana Ground clicked");
            Globals.PSConnections.Add(Globals.PowerSupplyInput.groundInput,Globals.BananaPlugs.B0);
            break;

            case("VoltageKnob"):
            OnPress(transform.Find("VoltageKnob").gameObject);
            OnRelease();
            break;

            default:
            Debug.Log("No buttono");
            break;

        }
    }

    public void OnPress(GameObject voltageKnob)
    {
        _rotate = true;
        Vector3 currRot = new Vector3(0,0,15.0f);
        Debug.Log("current rotation: " + currRot.y);
        voltageKnob.transform.Rotate(currRot);
        Debug.Log("On press");
    }
    
    public void OnRelease()
    {
        _rotate = false;
        Debug.Log("On Relase");
    }


}
