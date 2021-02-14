using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResistorSelect : SelectableItemBase

{
    [SerializeField] private List<Material> materialList = new List<Material>();
    [SerializeField] GameObject resistor = null;

    public int colorLabel;
    public override string Name
    {
        get
        {
            return "Resistor";
        }
    }

    public void Start()
    {
        resistor.GetComponent<Renderer>().material = materialList[colorLabel];
    }

    public override void onInteract()
    {
        Debug.Log("Resistor Value is value: " + colorLabel);
    }
}
