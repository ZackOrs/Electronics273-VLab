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

    public static Dictionary<AgilentInput,BananaPlugs> AgilentConnections = new Dictionary<AgilentInput,BananaPlugs>();
    // public static List<AvailableMachines> bananaSlot2Connections = new List<AvailableMachines>();
    // public static List<AvailableMachines> bananaSlot3Connections = new List<AvailableMachines>();
    // public static List<AvailableMachines> bananaSlot4Connections = new List<AvailableMachines>();
    // public static List<AvailableMachines> bananaSlot5Connections = new List<AvailableMachines>();

    public static Dictionary<PowerSupplyInput, BananaPlugs> PSConnections = new Dictionary<PowerSupplyInput, BananaPlugs>();
    public static Dictionary<FlukeInput, BananaPlugs> FlukeConnections = new Dictionary<FlukeInput, BananaPlugs>();


    public enum AvailableMachines
    {
        AgilentPos,
        AgilentNeg,
        PowerSupply,
        Oscilloscope
    }

    public enum FlukeInput
    {
        currentInput,
        currentMaxInput,
        voltageInput,
        groundInput,

    }
    public enum PowerSupplyInput
    {
        voltageSource,
        resistor,
        currentSource,
        ground,
        voltageKnob,
        currentKnob
    }
    public enum AgilentInput
    {
        currentInput,
        groundInput,
        voltageInput
    }

    public enum BananaPlugs
    {   
        //Refers to the banana plug number location on the breadboard

        //1
        B0,
        //2
        B1,
        //3
        B2,
        //4
        B3,
        //5
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
