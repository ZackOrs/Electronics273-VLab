using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapacitorSelect : SelectableItemBase
{
    //[SerializeField] private List<Material> materialList = new List<Material>();
    [SerializeField] GameObject capacitor = null;

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
        capacitor = this.gameObject;
        capacitor.GetComponent<Renderer>().material = Resources.Load<Material>("Default");
    }

    public override void onInteract()
    {
        Debug.Log("Capacitor Value is value: " + CapacitorValue);
    }
}
