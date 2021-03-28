using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OscilloscopeSelect : SelectableItemBase
{
     // [SerializeField] private string spawnableTag = "Spawnable";

    public GameObject OscilloscopePanel;

    public override string Name
    {
        get
        {
            return "Oscilloscope";
        }
    }
    
    public override void onInteract()
    {
        OpenOscilloscopePanel();
    }

    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    public void OpenOscilloscopePanel()
    {
        OscilloscopePanel.SetActive(true);

        Globals.showCrosshair = false;
        Globals.menuOpened = true;
        Globals.showCrosshair = false;

        Time.timeScale = 0.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }
    
    public void CloseOscilloscopePanel()
    {

        OscilloscopePanel.SetActive(false);

        Globals.showCrosshair = true;
        Globals.menuOpened = false;
        Globals.mouseClickAction = Globals.MouseClickAction.NoClick;

        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

    }

    public void CancelButton()
    {
        CloseOscilloscopePanel();
    }

}

