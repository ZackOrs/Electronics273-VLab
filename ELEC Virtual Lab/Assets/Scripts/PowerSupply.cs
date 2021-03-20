using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerSupply : MonoBehaviour
{
    public static GameObject plusButtonClicked;
    public static GameObject minusButtonClicked;
    [SerializeField] TMP_Text textBox = null;

    public float powerReading = 0.0f;

    public void IncreasePower()
    {
        powerReading += 1;
       
        UpdateText();
    }

    public void DecreasePower()
    {
        powerReading -= 1;
        UpdateText();
    }

    private void UpdateText()
    {
        textBox.text = powerReading.ToString("0.000") + " V";
    }
}
