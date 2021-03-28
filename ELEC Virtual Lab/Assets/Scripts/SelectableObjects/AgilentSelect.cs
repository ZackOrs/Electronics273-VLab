using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AgilentSelect : SelectableItemBase
{
    // [SerializeField] private string spawnableTag = "Spawnable";

    public GameObject AgilentPanel;

    public override string Name
    {
        get
        {
            return "Agilent";
        }
    }
    
    public override void onInteract()
    {
        OpenAgilentPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    public void OpenAgilentPanel()
    {
        AgilentPanel.SetActive(true);

        Globals.showCrosshair = false;
        Globals.menuOpened = true;
        Globals.showCrosshair = false;

        Time.timeScale = 0.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }
    
    public void CloseAgilentPanel()
    {

        AgilentPanel.SetActive(false);

        Globals.showCrosshair = true;
        Globals.menuOpened = false;
        Globals.mouseClickAction = Globals.MouseClickAction.NoClick;

        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

    }

    public void CancelButton()
    {
        CloseAgilentPanel();
    }

}
