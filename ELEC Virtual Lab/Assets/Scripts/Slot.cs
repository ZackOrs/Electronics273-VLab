using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    bool isFree = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PlaceItem()
    {
        if(isFree)
        {
            isFree = false;
            return true;
        }
        
        return false;
    }

    public void RemoveItem()
    {
        isFree = true;
    }
}
