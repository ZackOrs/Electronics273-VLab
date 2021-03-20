using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour
{
    public GameObject timeDisplay;
    public int hour;
    public int minute;
    public int second;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showTime()
    {  
        {
            hour = System.DateTime.Now.Hour;
            minute = System.DateTime.Now.Minute;
            second = System.DateTime.Now.Second;
            timeDisplay.GetComponent<Text>().text = "Time: " + hour + ":" + minute + ":" + second;
        }

    }
}
