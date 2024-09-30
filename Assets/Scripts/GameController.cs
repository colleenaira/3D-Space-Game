using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


/*
 * Class: GameController
 * Description: Manages game state like timers and level transitions. Previously handles UI updates and interacts with the ShipController for game events.
 * Handles end-game logic and logs game completion time and other statistics.
 * Usage: Attach this script to the GameManager GameObject in the MainMenu scene that controls game state across scenes.
 * Use Debug.Log statements for reviewing, identifying, and cleaning up any issues.
 */

public class GameController : MonoBehaviour
{

    // Singleton pattern to ensure only one instance of GameController exists.
    public static GameController Instance { get; private set; }

    [Header("UI Components")]
    public GameObject gameUI;
    public TextMeshProUGUI startPrompt;
    public Text endGamePrompt;      // Assign these in the Unity inspector.

    [Header("Game Stats")]
    private bool isGameStarted;
    public bool isGameEnded = false;
    private float gameStartTime;
    private float gameEndTime;

    public float currentTime;
    private bool isTimerRunning = false;


    AudioManager audioManager;
    private GateTriggerHandler gateTriggerHandler;


    /// This method is to clear the console every new scene as it can be overwhelming when checking for logs
    public class ClearUnityConsole
    {
        public static void ClearConsole()
        {
            // This simply does "LogEntries.Clear()" the long way:
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }


    // This method initializes the game environment.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the game controller persisting across scenes.
            DontDestroyOnLoad(gameUI);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Ensures only one instance is active.
        }
        audioManager = AudioManager.Instance;  // Setup reference to the audio manager.
    }

    void Start()
    {
        if (audioManager == null)
        {
            audioManager = AudioManager.Instance;
            if (audioManager == null)
            {
                Debug.LogError("AudioManager is still not initialized.");
            }
        }

        //Debug.Log("GameController: Start");
        ShipController shipController = FindObjectOfType<ShipController>();
        gateTriggerHandler = FindObjectOfType<GateTriggerHandler>();
     

        if (gateTriggerHandler != null)
        {
            gateTriggerHandler.OnAllGatesPassed += EndGame;  // Subscribe to the event when all gates are passed.
        }
    }


    // This method marks the game as started and updates the UI.
    public void SetGameStarted(bool hasStarted)
    {
        isGameStarted = hasStarted;
        if (hasStarted)
        {
            StartTimer();
        }
    }

    // This method initializes game start conditions.
    public void StartGame()
    {
        isGameStarted = true;
        // Loads first game scene...
    }


    // OnEnable and OnDisable manage scene loading/unloading behaviors.
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //Debug.Log("GameController: OnEnable");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (gateTriggerHandler != null)
        {
            gateTriggerHandler.OnAllGatesPassed -= EndGame;
        }
    }

    // This method resets UI and game state when a new scene loads.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (gateTriggerHandler != null)
        {
            gateTriggerHandler.ResetGates(); // Reset gatesPassed count
        }
        //ClearUnityConsole.ClearConsole(); 
    }


    public void StartTimer()
    {
        //Debug.Log("StartTimer called");
        isTimerRunning = true;
    }


    // This method is called once per frame to update time-sensitive components.
    void Update()
    {
        //Debug.Log("GameController: Update");
        if (isGameStarted && isTimerRunning)
        {
            //Debug.Log("GameController Update running");
            currentTime += Time.deltaTime;
        }
    }


    public void StopTimer()
    {
        isTimerRunning = false;
       /* if (hasLimit)
        {
            currentTime = timerLimit;
        }*/
    }

    public void DisplayCollisionCount()
    {
        ShipController shipController = FindObjectOfType<ShipController>();
        if (shipController != null)
        {
            int collisions = shipController.GetCollisionCount();
            Debug.Log("Total collisions in this scene: " + collisions);
        }
        else
        {
            Debug.Log("ShipController not found.");
        }
    }

   
    public void EndGame()
    {

        //Debug.Log("EndGame called - stopping timer.");
        StopTimer();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance is null.");
        }
        else if (audioManager.checkPoint == null)
        {
            Debug.LogError("checkPoint AudioClip is null.");
        }
        else
        {
            audioManager.PlaySFX(audioManager.checkPoint); // Play end game sound
        }

        isGameEnded = true;
        LogGameStats();
        Debug.Log("You completed the course!");
        SceneController.Instance.LoadNextScene();

    }


    // This method output game statistics at the end of each scene in the console.
    private void LogGameStats()
    {
        float totalCollisionTime = FindObjectOfType<ShipController>().collisionTime;
        Debug.Log($"End Game - Total collision time: {totalCollisionTime} seconds");

        int totalCollisions = FindObjectOfType<ShipController>().GetCollisionCount();
        Debug.Log($"End Game - Total collisions in this scene: {totalCollisions}");
    }

}