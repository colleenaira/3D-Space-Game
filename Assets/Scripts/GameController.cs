using UnityEngine;
using UnityEngine.UI; 
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
    AudioManager audioManager;
    public static GameController Instance { get; private set; }
    private GateTriggerHandler gateTriggerHandler;
    public GameObject gameUI;
    public Image healthBar;
    public TextMeshProUGUI startPrompt;
    public Text endGamePrompt;      // Assign in the inspector

    public float health;
    public float maxHealth;
    private bool isGameStarted;
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

        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager = AudioManager.Instance;

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

        InitializeUI();
    }
    public void InitializeUIComponents(TextMeshProUGUI timer)
    {
        this.timerText = timer;
        // Any other initialization code...
    }
    private void InitializeUI()
    {
      
        if (healthBar == null)
        {
            healthBar = GameObject.FindWithTag("HealthBarTag").GetComponent<Image>();
            if (healthBar != null)
            {
                healthBar.fillAmount = health / maxHealth;
            }
        }

        if (timerText == null)
        {
            timerText = GameObject.FindWithTag("TimerTextTag").GetComponent<TextMeshProUGUI>();
            if (timerText == null)
            {
                timerText = GameObject.FindWithTag("TimerTextTag").GetComponent<TextMeshProUGUI>();
                if (timerText != null)
                {
                    SetTimerText();
                    Debug.Log("TimerText found and initialized.");
                }
                else
                {
                    Debug.LogError("TimerText not found.");
                }
            }
         }

        ShowGameUI(isGameStarted);  // Hide the game UI if the game hasn't started yet.
    }


    public void SetGameStarted(bool hasStarted)
    {
        isGameStarted = hasStarted;
        ShowGameUI(hasStarted);
        if (hasStarted)
        {
            StartTimer();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        ShowGameUI(true);
        // Load first game scene...
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

        if (gateTriggerHandler != null)
        {
            gateTriggerHandler.ResetGates(); // Reset gatesPassed count
        }


        InitializeUI();
        ResetUI();
    }

    public void StartTimer()
    {
        Debug.Log("StartTimer called");
        isTimerRunning = true;
    }

    void Update()
    {
        if (isGameStarted && isTimerRunning)
        {
            currentTime += Time.deltaTime;
            SetTimerText();
        }
    }

    public void ResetUI()
    {
        // Reset health
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.fillAmount = health / maxHealth;
        }
        else
        {
            Debug.LogError("HealthBar not found");
        }

        // Reset timer
        currentTime = 0f;
        if (timerText != null)
        {
            SetTimerText();
        }
        else
        {
            Debug.LogError("TimerText not found");
        }
        // Ensure the UI elements are in their default state
        ShowGameUI(false);
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
        timerText.color = Color.red;
        isTimerRunning = false;
        if (hasLimit)
        {
            currentTime = timerLimit;
            SetTimerText();
        }
    }

    public void StopHealthDecrease()
    {
        isGameEnded = true; // Set the flag to stop health decrease in UpdateHealth method.
    }


    public void UpdateHealth(float amount)
    {

        if (!isGameEnded && GateTriggerHandler.Instance.gatesPassed < GateTriggerHandler.Instance.totalGates)
        {
            health -= amount;
            health = Mathf.Max(health, 5); // Ensure health doesn't drop below 5
            healthBar.fillAmount = health / maxHealth;
        }
        else
        {
            Debug.Log("Not Updating health");
        }

    }

    public void ShowGameUI(bool show)
    {
        healthBar.gameObject.SetActive(show);
        timerText.gameObject.SetActive(show);
    }

 
    public void EndGame()
    {
        Debug.Log("EndGame called - stopping timer.");
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
        Debug.Log("You completed the course!");
        SceneController.Instance.LoadNextScene();

    }
}
