using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject MessagePanel;

    public void OpenMessagePanel(string Text)
    {
        MessagePanel.SetActive(true);
    } 
    
    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
