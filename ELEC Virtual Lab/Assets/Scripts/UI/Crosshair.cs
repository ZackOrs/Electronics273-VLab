using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    public GameObject gameCrosshair;

    void Update()
    {
        if(!Globals.showCrosshair)
        {
            gameCrosshair.SetActive(false);
        }
        else
        {
            gameCrosshair.SetActive(true);
        }
    }
}
