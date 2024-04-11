using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private List<int> sceneOrder = new List<int>();
    private int currentSceneIndex = 0;
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

    private void InitializeScenes()
    {
        // Assuming you have 8 scenes for the conditions
        for (int i = 1; i <= 24; i++)
        {
            sceneOrder.Add(i);
            scenePlayCount[i] = 0;
        }
    }

    public void LoadNextScene()
    {
        if (currentSceneIndex >= sceneOrder.Count)
        {
            if (currentSceneIndex >= sceneOrder.Count)
            {
                Debug.Log("All scenes completed, going to the main menu.");
                SceneManager.LoadScene("MainMenu"); // Replace with the actual name of your main menu scene.
                return;
            }

            SceneManager.LoadScene(sceneOrder[currentSceneIndex]);
            currentSceneIndex++;
        }
    }


    // Call this method from the end of each trial/scene
    public void OnSceneEnd()
    {
        LoadNextScene();
    }

    public void ResetSceneIndex()
    {
        currentSceneIndex = 0; // Reset index to start from the first scene.
    }

}
