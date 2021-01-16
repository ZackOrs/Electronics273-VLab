using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static bool gamePaused = false;
    public static bool menuOpened = false;
    public static bool showCrosshair = true;

    public static List<SpawnableItem> inventoryItems= new List<SpawnableItem>();

}
