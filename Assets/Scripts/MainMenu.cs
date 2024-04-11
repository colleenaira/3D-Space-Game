using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        // Reset the scene index if you are starting a new game cycle.
        SceneController.Instance.ResetSceneIndex();

        // Load the first scene or let the SceneController decide which scene to load.
        SceneController.Instance.LoadNextScene();

        // Tell GameController to start the game.
        GameController.Instance.SetGameStarted(true);
    }

    public void Quit() 
    {
        Application.Quit();
  
    }

}
