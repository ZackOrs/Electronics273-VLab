using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static string currentMachine = "";
    public static bool gamePaused = false;
    public static bool menuOpened = false;
    public static bool showCrosshair = true;
    public static bool cameraAttachedToPlayer = true;
    public static int itemIDCount = 0;

    public static bool lookingAtFocusableObject = false;

    public static MouseClickAction mouseClickAction;

    public static List<SpawnableItem> inventoryItems = new List<SpawnableItem>();

    public static Dictionary<AgilentButtons,BananaPlugs> AgilentConnections = new Dictionary<AgilentButtons,BananaPlugs>();
    // public static List<AvailableMachines> bananaSlot2Connections = new List<AvailableMachines>();
    // public static List<AvailableMachines> bananaSlot3Connections = new List<AvailableMachines>();
    // public static List<AvailableMachines> bananaSlot4Connections = new List<AvailableMachines>();
    // public static List<AvailableMachines> bananaSlot5Connections = new List<AvailableMachines>();


    public enum AvailableMachines
    {
        AgilentPos,
        AgilentNeg,
        PowerSuplly,
        Oscilloscope
    }

    public enum AgilentButtons
    {
        currentInput,
        groundInput,
        voltageInput
    }

    public enum BananaPlugs
    {
        B0,
        B1,
        B2,
        B3,
        B4,
        noConnection
    }
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
        powerSupplySlot,
        BananaPlugSlot
    }

}
