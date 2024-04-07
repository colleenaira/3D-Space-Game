using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneController.Instance.LoadNextScene();
        GameController.Instance.StartGame();

    }

    public void Quit() 
    {
        Application.Quit();
  
    }

}
