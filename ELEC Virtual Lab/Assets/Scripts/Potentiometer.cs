using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Potentiometer : MonoBehaviour
{
    public TMP_InputField text;
    [SerializeField] TMP_Text textBox = null;



    public int powerReading = 0;


   

    private void UpdateText()
    {
        textBox.text = powerReading.ToString("0") + " Ω";
    }

}


