using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace
using TMPro;
using System.Collections.Generic;
using System;

// Controls the timer and health UI
// ShipController: Health changes upon collisions
// GateTriggerHandler: for level completion 


public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Image healthBar;
    public float health;            // Starting health
    public float maxHealth;
    public Text endGamePrompt;      // Assign in the inspector

    
    private bool isGameEnded = false;
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

    private void OnDisable()
    {
 
        // Unsubscribe to prevent memory leaks
        if (gateTriggerHandler != null)
        {
            gateTriggerHandler.OnAllGatesPassed -= EndGame;
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

        // If first countdown is true then currentTime -, else currentTime + 
        //currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

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
        health -= amount;
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        // Example condition for ending the game - you would check if the last gate is passed instead
        if (health <= 0 && !isGameEnded)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("EndGame called - stopping timer.");

        StopTimer();
        isGameEnded = true;

        audioManager.StopMusic();                                // Stop BGM
        audioManager.PlaySFX(audioManager.checkPoint);          // Play end game sound

        Debug.Log("You completed the course!");


        // Show end game UI or perform other end game actions

    }
}
