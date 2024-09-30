using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Class: SceneController
 * Description: Manages scene transitions within the game, including loading next and previous scenes, and returning to main menu.
 * Usage: Attach this script to a SceneController GameObject in the Main Menu Scene Manager GameObject.
 * Use Debug.Log statements for reviewing, identifying, and cleaning up any issues.
 */


public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private List<int> sceneOrder = new List<int>();
    private Dictionary<int, int> scenePlayCount = new Dictionary<int, int>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeScenes();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// Initialize scene order and other scene-related settings
    private void InitializeScenes()
    {
        for (int i = 1; i <= 25; i++)
        {
            sceneOrder.Add(i);
            scenePlayCount[i] = 0;
        }
    }

    /// Handles keyboard inputs for scene transitions
    void Update()
    {
        // Press 'N' to load the next scene
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }
        // Press 'P' to load the previous scene (optional for testing)
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadPreviousScene(); 
        }
        // Press 'U' to restart current scene (optional for testing)
        if (Input.GetKeyDown(KeyCode.U))
        {
            RestartCurrentScene();
        }
        // Press 'M' to return to main menu (optional for testing)
        if (Input.GetKeyDown(KeyCode.M))
        {
            ReturnToMainMenu();
        }
    }


    public void LoadNextScene()
    {
        // Check if the current scene is the last in the build order
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // If it's the last scene, loop back to the main menu (assuming its at build index 0)
            SceneManager.LoadScene(0);
        }
        else
        {
            // If it's not the last scene, load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void LoadPreviousScene()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (previousSceneIndex < 0)
        {
            previousSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        }
        SceneManager.LoadScene(previousSceneIndex);
    }

    void RestartCurrentScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Debug.Log("Scene restarted.");
    }

    void ReturnToMainMenu()
    {
        // Load the main menu scene (assuming it's at build index 0)
        SceneManager.LoadScene(0);
        //Debug.Log("Returned to main menu.");
    }


    // Call this method from the end of each trial/scene
    public void OnSceneEnd()
    {
        LoadNextScene();
    }

/*    public void ResetSceneIndex()
    {
        currentSceneIndex = 0; // Reset index to start from the first scene.
    }
*/
}
