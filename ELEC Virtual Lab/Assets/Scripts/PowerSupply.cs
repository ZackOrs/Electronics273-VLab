using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerSupply : MonoBehaviour
{
    public static GameObject plusButtonClicked;
    public static GameObject minusButtonClicked;

    [SerializeField] GameObject posSlot = null;
    [SerializeField] GameObject negSlot = null;

    [SerializeField] TMP_Text textBox = null;

    public float powerReading = 0.0f;

    public void IncreasePower()
    {
        float posPower = 0;
        float negPower = 0;

        powerReading += 1;
        posPower = powerReading;
        negPower = 0;

        posSlot.GetComponent<Slot>().voltage = posPower;
        if (posSlot.GetComponent<Slot>().itemPlaced != null)
        {
            posSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage = posSlot.GetComponent<Slot>().voltage;
        }
        Debug.Log("Pos power:" + posPower);

        negSlot.GetComponent<Slot>().voltage = negPower;
        if (negSlot.GetComponent<Slot>().itemPlaced != null)
        {
            negSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage = negSlot.GetComponent<Slot>().voltage;
        }
        Debug.Log("Neg power:" + negPower);

        UpdateText();
    }

    public void DecreasePower()
    {
        float posPower = 0;
        float negPower = 0;

        powerReading -= 1; ;
        posPower = powerReading;
        negPower = 0;

        posSlot.GetComponent<Slot>().voltage = posPower;
        if (posSlot.GetComponent<Slot>().itemPlaced != null)
        {
            posSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage = posSlot.GetComponent<Slot>().voltage;
        }
        Debug.Log("Pos power:" + posPower);

        negSlot.GetComponent<Slot>().voltage = negPower;
        if (negSlot.GetComponent<Slot>().itemPlaced != null)
        {
            negSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage = negSlot.GetComponent<Slot>().voltage;
        }
        Debug.Log("Neg power:" + negPower);

        UpdateText();
    }

    private void UpdateText()
    {
        textBox.text = powerReading.ToString("0.000") + " V";
    }
}
