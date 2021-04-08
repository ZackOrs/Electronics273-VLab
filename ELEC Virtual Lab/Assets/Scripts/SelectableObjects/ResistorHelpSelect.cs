using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResistorHelpSelect : SelectableItemBase
{
    [SerializeField] GameObject focusPoint = null;

    public override string Name
    {
        get
        {
            return "ResistorHelp";
        }
    }
    
    public override void onInteract()
    {
        Globals.lookingAtFocusableObject = true;
        Camera.main.GetComponent<AnimateCamera>().targetObject = focusPoint;
    }

    void Update()
    {

    }
}
