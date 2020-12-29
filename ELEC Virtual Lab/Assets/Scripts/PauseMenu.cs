using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gamePaused = false;

    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {

            if(gamePaused)
            {
                ResumeGame();
            }   
            else
                PauseGame();

        }
    }


    private void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}