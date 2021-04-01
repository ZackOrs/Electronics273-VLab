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
            case("BtnDCV.023"):
            Button23Pressed();
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