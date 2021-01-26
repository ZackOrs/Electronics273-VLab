using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isFree = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceItem()
    {
        if(isFree)
        {
            if(CursorStyle.breadbBoardItemSelectedClickCount== 1)
            {
                //Code to say this is starting point of object
                isFree = false;
                CursorStyle.breadbBoardItemSelectedClickCount = 2;
            }
            else if(CursorStyle.breadbBoardItemSelectedClickCount == 2)
            {
                //code to say this is ending point of object
                isFree = false;
                CursorStyle.breadbBoardItemSelectedClickCount = 0;
            }
            else
            {
                Debug.Log("No item selected");
            }
        }
        else
        {
            Debug.Log("Slot is no free");
        }
    }

    public void RemoveItem()
    {
        isFree = true;
    }
}
