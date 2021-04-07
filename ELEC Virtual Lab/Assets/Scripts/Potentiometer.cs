using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Potentiometer : MonoBehaviour
{
    public static GameObject plusButtonClicked;
    public static GameObject minusButtonClicked;
    [SerializeField] TMP_Text textBox = null;



    public int powerReading = 0;


    public void MoreResistance()
    {
        if (powerReading==500)
        {

            plusButtonClicked.SetActive(false);
        }
        else
        {

            powerReading += 50;

            UpdateText();
        }
        
    }

    public void LessResistance()
    {

        if (powerReading==0)
        {

            minusButtonClicked.SetActive(false);
        }
        else
        {
            powerReading -= 50;
            UpdateText();
        }
        
    }

    private void UpdateText()
    {
        textBox.text = powerReading.ToString("0") + " Ω";
    }

}


