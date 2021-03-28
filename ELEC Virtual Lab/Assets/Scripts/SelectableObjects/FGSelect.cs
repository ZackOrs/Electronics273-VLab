using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FGSelect : SelectableItemBase
{
   // [SerializeField] private string spawnableTag = "Spawnable";

    public GameObject FGPanel;
    public override string Name
    {
        get
        {
            return "FG";
        }
    }
    
    public override void onInteract()
    {
        OpenFGPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    public void OpenFGPanel()
    {
        FGPanel.SetActive(true);

        Globals.showCrosshair = false;
        Globals.menuOpened = true;
        Globals.showCrosshair = false;

        Time.timeScale = 0.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }
    
    public void CloseFGPanel()
    {

        FGPanel.SetActive(false);

        Globals.showCrosshair = true;
        Globals.menuOpened = false;
        Globals.mouseClickAction = Globals.MouseClickAction.NoClick;

        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

    }

    public void CancelButton()
    {
        CloseFGPanel();
    }

}

