using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{


    public void changeToScene(int changeTheScene){
        
        SceneManager.LoadScene(changeTheScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Application...");
        Application.Quit();

    }
}

