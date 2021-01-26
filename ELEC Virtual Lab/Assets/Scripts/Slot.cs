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
        if (isFree)
        {

            switch (Globals.mouseClickAction)
            {
                case Globals.MouseClickAction.TwoClicks_FirstClick:
                    isFree = false;
                    Globals.mouseClickAction = Globals.MouseClickAction.TwoClicks_SecondClick;
                    break;

                case Globals.MouseClickAction.TwoClicks_SecondClick:
                    isFree = false;
                    Globals.mouseClickAction = Globals.MouseClickAction.NoClick;
                    break;
                default:
                    Debug.Log("No item selected");
                    break;
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
