// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;

// public class Voltmeter : MonoBehaviour
// {

//     [SerializeField] GameObject posSlot = null;
//     [SerializeField] GameObject negSlot = null;

//     [SerializeField] TMP_Text textBox = null;

//     private float voltageReading = 0;
//     static Voltmeter voltmeter = new Voltmeter();

//     Voltmeter()
//     {
        
//     }

//     static public void UpdateTerminals()
//     {
//         float posVoltage = 0;
//         float negVoltage = 0;
//         if(voltmeter.posSlot != null)
//         {
//             voltmeter.posSlot.GetComponent<Slot>().voltage = voltmeter.posSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage;
//             posVoltage = voltmeter.posSlot.GetComponent<Slot>().voltage;
//             Debug.Log("Pos voltage:" + posVoltage);
//         }
//         if(voltmeter.negSlot != null)
//         {
//             voltmeter.negSlot.GetComponent<Slot>().voltage = voltmeter.posSlot.GetComponent<Slot>().slotPair.GetComponent<Slot>().voltage;
//             negVoltage = voltmeter.negSlot.GetComponent<Slot>().voltage;
//             Debug.Log("Neg voltage:" + negVoltage);
//         }

//         if(voltmeter.posSlot != null && voltmeter.negSlot != null)
//         {
//             voltmeter.voltageReading = posVoltage - negVoltage;
//         }
//         voltmeter.UpdateText();
//     }

//     private void UpdateText()
//     {
//         Debug.Log("Voltage drop value:" + voltageReading);
//         // voltmeter.textBox.text = voltageReading.ToString() + " V";
//     }

// }
