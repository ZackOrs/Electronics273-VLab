using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreadboardSelect : SelectableItemBase
{

    [SerializeField] private string spawnableTag = "Spawnable";
    public GameObject BreadboardPanel;
    

    public override string Name
    {
        get
        {
            return "Breadboard";
        }
    }

    public override void onInteract()
    { 
        OpenBreadboardPanel();
    }

    public void OpenBreadboardPanel()
    {
        BreadboardPanel.SetActive(true);
        
        Globals.showCrosshair = false;
        Globals.menuOpened = true;
        Globals.showCrosshair = false;

        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }     

    public void CloseBreadboardPanel()
    {
        BreadboardPanel.SetActive(false);

        Globals.showCrosshair = true;
        Globals.menuOpened = false;

        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Button functions
    public void CancelButton()
    {
        CloseBreadboardPanel();
    }

    public void ResetButton()
    {
        //TODO: IMPLEMENT CLEAR, probably add a tag to all created objects and then remove them
        Debug.Log("Clearing table");
    }

    public void ConfirmButton()
    {
        //TODO: Impletement confirm, probably make a list containing all objects and then create them as needed
        Debug.Log("Confirm Selection");
        CloseBreadboardPanel();
    }
}