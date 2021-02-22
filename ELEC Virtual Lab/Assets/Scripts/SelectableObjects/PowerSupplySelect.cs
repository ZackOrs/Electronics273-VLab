using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerSupplySelect : SelectableItemBase
{
    [SerializeField] private GameObject PowerSupplyPanel = null;
    // Start is called before the first frame update

    public override string Name
    {
        get
        {
            return "Power Supply";
        }
    }

    public override void onInteract()
    {
        OpenPowerSupplyPanel();
    }

    public void OpenPowerSupplyPanel()
    {
       PowerSupplyPanel.SetActive(true);

        Globals.showCrosshair = false;
        Globals.menuOpened = true;
        Globals.showCrosshair = false;

        Time.timeScale = 0.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }

    private void ClosePanel()
    {
        PowerSupplyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }
}
