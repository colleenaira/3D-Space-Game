using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private List<int> sceneOrder = new List<int>();
    private int currentSceneIndex = 0;
    private int repetitions = 2; // Number of repetitions for each scene
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
        for (int i = 1; i <= 4; i++)
        {
            sceneOrder.Add(i);
            scenePlayCount[i] = 0;
        }

        ShuffleScenes();
    }

    public void LoadNextScene()
    {
        if (currentSceneIndex >= sceneOrder.Count)
        {
            // All scenes have been played once, check repetitions
            bool allRepetitionsDone = true;
            foreach (var count in scenePlayCount.Values)
            {
                if (count < repetitions)
                {
                    allRepetitionsDone = false;
                    break;
                }
            }

            if (allRepetitionsDone)
            {
                // All scenes have been played the required number of times
                Debug.Log("All trials completed.");
                // Load end screen or main menu
                SceneManager.LoadScene(0); // Replace with your end scene index or main menu
                return;
            }

            // Reshuffle and restart the sequence
            ShuffleScenes();
            currentSceneIndex = 0;
        }

        // Load the next scene
        int sceneToLoad = sceneOrder[currentSceneIndex];
        SceneManager.LoadScene(sceneToLoad);
        scenePlayCount[sceneToLoad]++;
        currentSceneIndex++;
    }

    private void ShuffleScenes()
    {
        for (int i = 0; i < sceneOrder.Count; i++)
        {
            int temp = sceneOrder[i];
            int randomIndex = Random.Range(i, sceneOrder.Count);
            sceneOrder[i] = sceneOrder[randomIndex];
            sceneOrder[randomIndex] = temp;
        }
    }

    // Call this method from the end of each trial/scene
    public void OnSceneEnd()
    {
        LoadNextScene();
    }
}
