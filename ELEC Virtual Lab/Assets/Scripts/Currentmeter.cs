using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currentmeter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject posSlot = null;
    public GameObject negSlot = null;

    [SerializeField] TMP_Text textBox = null;

    public void UpdateText(float currentReading)
    {
        if(posSlot.GetComponent<Slot>().itemPlaced != null && negSlot.GetComponent<Slot>().itemPlaced != null)
        {
            textBox.text = currentReading.ToString("0.000") + " A";
        }
        
        else
        {
            textBox.text = "NO READING";
        }
    }
    
}
