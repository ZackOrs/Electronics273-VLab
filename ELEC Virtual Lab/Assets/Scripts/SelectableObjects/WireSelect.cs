using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WireSelect : SelectableItemBase
{
    [SerializeField] private List<Material> materialList = new List<Material>();
    [SerializeField] GameObject wire = null;

    public int Colour;
    public override string Name
    {
        get
        {
            return "Wire";
        }
    }

    public void Start()
    {
        wire.GetComponent<Renderer>().material = materialList[Colour];
    }

    public override void onInteract()
    { 
        Debug.Log("Colour is value: " + Colour);
    }

}
