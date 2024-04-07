using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

// Controls the timer and health UI
// ShipController: Health changes upon collisions
// GateTriggerHandler: for level completion 


public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject gameUI;
    
    public Image healthBar;
    public float health;            // Starting health
    public float maxHealth;

    public TextMeshProUGUI startPrompt;
    public Text endGamePrompt;      // Assign in the inspector

    
    public bool isGameEnded = false;
    private bool isTimerRunning = false;


    [Header("Component")]
    public TextMeshProUGUI timerText;
    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown = false;
    [Header("Timer Limit")]
    public bool hasLimit;
    public float timerLimit;
 
    private GateTriggerHandler gateTriggerHandler;

    AudioManager audioManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(gameUI);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        maxHealth = health; 
        ShipController shipController = FindObjectOfType<ShipController>();

        gateTriggerHandler = FindObjectOfType<GateTriggerHandler>();
     

        if (gateTriggerHandler != null)
        {
            gateTriggerHandler.OnAllGatesPassed += EndGame;
        }
    }

    public void StartGame()
    {
        ShowGameUI(true);
        // Load first game scene...
    }



    public void StopHealthDecrease()
    {
        isGameEnded = true; // Set the flag to stop health decrease in UpdateHealth method.
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Unsubscribe to prevent memory leaks
        if (gateTriggerHandler != null)
        {
            gateTriggerHandler.OnAllGatesPassed -= EndGame;
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the timerText again in the new scene
        if (timerText != null)
        {
            timerText = GameObject.FindWithTag("TimerTextTag").GetComponent<TextMeshProUGUI>();

        }
        else
        {
            Debug.LogError("The TimerText object was not found in the new scene.");
        }
    }

    public void StartTimer()
    {
        // Set the timer to be active
        isTimerRunning = true;
        countDown = false; // Assuming you want to count down
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime; // Only count up when the timer is running
        }

        if(hasLimit && (countDown && currentTime <= timerLimit || (!countDown && currentTime >= timerLimit))) 
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false; 
        }

        SetTimerText();

    }

    public void SetTimerText()
    {
        if (timerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = string.Format("{0:D2}:{1:D2}.{2:D2}",
                       timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
        else
        {
            Debug.LogError("Timer Text is not assigned in GameController.");
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }


    public void UpdateHealth(float amount)
    {

        if (!isGameEnded && GateTriggerHandler.Instance.gatesPassed < GateTriggerHandler.Instance.totalGates)
        {
            health -= amount;
            health = Mathf.Max(health, 5); // Ensure health doesn't drop below 5
            healthBar.fillAmount = health / maxHealth;
        }

        // If health hits the minimum and all gates haven't been passed, you may want to handle that scenario.
        if (health == 5 && GateTriggerHandler.Instance.gatesPassed < GateTriggerHandler.Instance.totalGates)
        {
            // Maybe trigger a warning or some effect to show that health is critical.
        }

    }

    public void ShowGameUI(bool show)
    {
        healthBar.gameObject.SetActive(show);
        timerText.gameObject.SetActive(show);
        startPrompt.gameObject.SetActive(!show);
    }

 
    public void EndGame()
    {
        Debug.Log("EndGame called - stopping timer.");

        StopTimer();
        isGameEnded = true;

        audioManager.StopMusic();                                // Stop BGM
        audioManager.PlaySFX(audioManager.checkPoint);          // Play end game sound

        Debug.Log("You completed the course!");
        SceneController.Instance.LoadNextScene();

    }
}
