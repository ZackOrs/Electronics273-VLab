using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapacitorSelect : SelectableItemBase
{
    [SerializeField] private List<Material> materialList = new List<Material>();
    [SerializeField] GameObject resistor = null;

    public int CapacitorValue;
    public override string Name
    {
        get
        {
            return "Capacitor";
        }
    }

    public void Start()
    {
        resistor.GetComponent<Renderer>().material = materialList[CapacitorValue];
    }

    public override void onInteract()
    {
        Debug.Log("Capacitor Value is value: " + CapacitorValue);
    }
}
