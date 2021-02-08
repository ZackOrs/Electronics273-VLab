using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static bool gamePaused = false;
    public static bool menuOpened = false;
    public static bool showCrosshair = true;

    public static int itemIDCount = 0;

    public static List<SpawnableItem> inventoryItems = new List<SpawnableItem>();

    public enum availableItems
    {
        Wire,
        Resistor,
        Capacitor
    }

    public static IEnumerator WaitForTimeInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);        
    }

}
