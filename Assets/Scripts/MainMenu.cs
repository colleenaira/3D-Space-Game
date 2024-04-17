using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("PlayGame method called");
        SceneController.Instance.LoadNextScene();

        // Load the first scene or let the SceneController decide which scene to load.

        if (GameController.Instance != null)
        {
            GameController.Instance.SetGameStarted(true);
        }
        else
        {
            Debug.LogError("GameController instance not found.");
        }
    }

    public void Quit() 
    {
        Application.Quit();
  
    }

}
