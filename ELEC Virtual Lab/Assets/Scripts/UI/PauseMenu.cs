using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {

            if(Globals.gamePaused)
            {
                ResumeGame();
            }   
            else
                PauseGame();
        }
    }


    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Globals.gamePaused = false;

        if(!Globals.menuOpened)
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Globals.gamePaused = true;
    }

    public void GoToSettingsMenu()
    {
        //TODO: IMPLEMENT SETTINGS
        Debug.Log("Going to settings...");
        SceneManager.LoadScene(3);
        
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Application...");
        SceneManager.LoadScene(0);

    }
}