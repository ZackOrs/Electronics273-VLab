using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static bool gamePaused = false;
    public static bool menuOpened = false;
    public static bool showCrosshair = true;
    public static bool cameraAttachedToPlayer = true;
    public static int itemIDCount = 0;

    public static bool lookingAtObject = false;

    public static MouseClickAction mouseClickAction;

    public static List<SpawnableItem> inventoryItems = new List<SpawnableItem>();

    public enum AvailableItems
    {
        Wire,
        Resistor,
        Capacitor
    }

    public enum MouseClickAction
    {
        NoClick = 0,
        TwoClicks_FirstClick = 1,
        TwoClicks_SecondClick = 2
    }

    public static IEnumerator WaitForTimeInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);        
    }
    
    public enum SlotType
    {
        defaultSlot,
        startSlot,
        groundSlot,
        voltmeterSlot,
        currentmeterSlot,
        powerSupplySlot
    }

}
