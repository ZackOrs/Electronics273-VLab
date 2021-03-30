using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OscilloscopeSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";

    public GameObject Camera;
    [SerializeField] GameObject focusPoint = null;

    public override string Name
    {
        get
        {
            return "Oscilloscope";
        }
    }
    
    public override void onInteract()
    {
        Globals.lookingAtFocusableObject = true;
        Camera.GetComponent<AnimateCamera>().targetObject = focusPoint;
    }

    void Update()
    {

    }


}
