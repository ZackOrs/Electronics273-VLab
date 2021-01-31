using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool isFree = true;
    [SerializeField] private Button button = null;
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
                    ChangeHighlightColor();
                    break;

                case Globals.MouseClickAction.TwoClicks_SecondClick:
                    isFree = false;
                    Globals.mouseClickAction = Globals.MouseClickAction.NoClick;
                    ChangeHighlightColor();
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
        ChangeHighlightColor();
    }


    private void ChangeHighlightColor()
    {
        Debug.Log("Color swapped");
        if (isFree)
        {
            ColorBlock colors = button.colors;
            colors.highlightedColor = new Color( 0.3f, 0.4f, 1.0f, 1.0f);
            button.colors = colors;
        }
        else
        {
            ColorBlock colors = button.colors;
            colors.highlightedColor = new Color(1.0f, 0.4f, 0.3f, 1.0f);
            button.colors = colors;
        }
    }
}
