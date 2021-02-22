using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResistorSelect : SelectableItemBase

{
    // [SerializeField] private List<Material> materialList = new List<Material>();
    [SerializeField] GameObject resistor = null;

    public int resistorLabel;
    public override string Name
    {
        get
        {
            return "Resistor";
        }
    }

    public void Start()
    {
        resistor = this.gameObject;
        resistor.GetComponent<Renderer>().material = Resources.Load<Material>("Default");
    }

    public override void onInteract()
    {
        Debug.Log("Resistor Value is : " + resistorLabel);
    }
}
