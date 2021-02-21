using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;
    public bool isFree = true;
    public Globals.SlotType slotType = default;
    public bool componentAdded = false;
    public SpawnableItem itemPlaced;
    [SerializeField] private Button button = null;

    public GameObject slotPair;

    public bool slotChecked = false;

    public float voltage = -1.0f;

    void Start()
    {

        switch (slotType)
        {
            case (Globals.SlotType.startSlot):
                voltage = 5.0f;
                break;

            case (Globals.SlotType.groundSlot):
                voltage = 0.0f;
                break;

            default:
                voltage = -1.0f;
                break;
        }
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
            Debug.Log("Slot is not free");
        }
    }

    public void RemoveItem()
    {
        isFree = true;
        slotPair = null;
        itemPlaced = null;

        ChangeHighlightColor();
    }


    private void ChangeHighlightColor()
    {

        if (isFree)
        {
            ColorBlock colors = button.colors;
            colors.highlightedColor = new Color(0.3f, 0.4f, 1.0f, 1.0f);
            button.colors = colors;
        }
        else
        {
            ColorBlock colors = button.colors;
            colors.highlightedColor = new Color(1.0f, 0.4f, 0.3f, 1.0f);
            button.colors = colors;
        }
    }

    private void OnRightClick()
    {
        //TODO: REMOVE PLACED ITEM
    }
}
