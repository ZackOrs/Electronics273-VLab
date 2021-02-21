using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Voltmeter : MonoBehaviour
{
    [SerializeField] GameObject posSlot = null;
    [SerializeField] GameObject negSlot = null;

    [SerializeField] TMP_Text textBox = null;

    private float voltageReading = 0;

    public void UpdateTerminals()
    {
        float posVoltage = 0;
        float negVoltage = 0;
        if(posSlot.GetComponent<Slot>().itemPlaced != null)
        {
            posSlot.GetComponent<Slot>().voltage = posSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage;
            posVoltage = posSlot.GetComponent<Slot>().voltage;
            Debug.Log("Pos voltage:" + posVoltage);
        }
        if(negSlot.GetComponent<Slot>().itemPlaced != null)
        {
            negSlot.GetComponent<Slot>().voltage = negSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage;
            negVoltage = negSlot.GetComponent<Slot>().voltage;
            Debug.Log("Neg voltage:" + negVoltage);
        }

        if(posSlot.GetComponent<Slot>().itemPlaced != null && negSlot.GetComponent<Slot>().itemPlaced != null)
        {
            voltageReading = posVoltage - negVoltage;
            UpdateText();
        }
        else
        {
            textBox.text = "NO READING";
        }
        
    }

    private void UpdateText()
    {
        textBox.text = voltageReading.ToString() + " V";
    }

}
