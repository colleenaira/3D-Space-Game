using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Class: MainMenu
 * Description: Provides functionality for main menu options like starting the game and quitting.
 * Usage: Attach this script to the MainMenu GameObject in the MainMenu scene.
 * Use Debug.Log statements for reviewing, identifying, and cleaning up any issues. 
 */

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Debug.Log("PlayGame method called");
        SceneController.Instance.LoadNextScene();

        // Loads the first scene and sets the game state. 

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
