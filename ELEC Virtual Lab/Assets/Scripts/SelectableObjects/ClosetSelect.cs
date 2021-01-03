using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClosetSelect : SelectableItemBase
{

    [SerializeField] private string spawnableTag = "Spawnable";
    public GameObject ClosetPanel;
    

    public override string Name
    {
        get
        {
            return "Closet";
        }
    }

    public override void onInteract()
    { 
        OpenClosetPanel();
    }

    public void OpenClosetPanel()
    {
        ClosetPanel.SetActive(true);
        
        Globals.showCrosshair = false;
        Globals.menuOpened = true;
        Globals.showCrosshair = false;

        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }       
    public void CloseClosetPanel()
    {
        ClosetPanel.SetActive(false);

        Globals.showCrosshair = true;
        Globals.menuOpened = false;

        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Button functions
    public void CancelButton()
    {
        CloseClosetPanel();
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
        SpawnSelectedObects();
        CloseClosetPanel();
    }


    private void SpawnSelectedObects()
    {
        for (int i = 0 ; i < ClosetPanel.transform.childCount ; i ++)
        {
            var child = ClosetPanel.transform.GetChild(i);
            if(child.CompareTag(spawnableTag))
            {
                ISpawnableItem item = child.GetComponent<ISpawnableItem>();
                item.onSpawn();
            }
        }
    }
}