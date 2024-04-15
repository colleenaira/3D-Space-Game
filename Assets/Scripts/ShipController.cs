using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ShipController : MonoBehaviour
{
    AudioManager audioManager;

    public Transform thisShip;
    public Rigidbody rb;
    public GameObject gameUI;
    public GameController gameController;


    public float turnSpeed = 60f;
    public float moveSpeed = 45f;
    public float damage;

    public bool isGameStarted = false; // Added flag for game start

    private void Awake()
    {
        audioManager = AudioManager.Instance; // Instead of using GameObject.FindGameObjectWithTag
    }
    void Start()
    {
  
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space)) // Press spacebar to Start
        {
            StartGame();
        }

        if (!isGameStarted) return;         //Collect input in Update
    }

    public void StartGame()
    {        
        isGameStarted = true;
        gameUI.SetActive(false);         // Disable the main menu
        GameController.Instance.SetGameStarted(true);
        GameController.Instance.ResetUI();
        gameUI.SetActive(false);


    }

    void FixedUpdate()
    {
        if (!isGameStarted) return;
        Turn();
        Thrust();

    }

    void Turn()
    {
        // Gather input for yaw (turn), pitch (nose up/down), and roll (tilt)
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Rotate");

        // Rotate the ship based on input
        thisShip.Rotate(pitch, yaw, roll);
    }

    void Thrust()
    {
        // Move the ship forward continuously in the direction it's facing
        rb.velocity = transform.forward * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if collided with Wall
        if (other.CompareTag("Wall"))
        {
            // Process as a wall collision but also count it as a gate pass
            GameController.Instance.UpdateHealth(damage); // Apply damage or any other collision effect
            audioManager.PlaySFX(audioManager.wallTouch); // Play collision sound effect
            GateTriggerHandler.Instance.GatePassed(); // Count as gate passed
        }
        else if (other.CompareTag("Gate"))
        {
            // Process as a gate pass without collision
            GateTriggerHandler.Instance.GatePassed(); // Count as gate passed
            audioManager.PlaySFX(audioManager.gateIn); // Play gate pass sound effect
        }
        else if (other.CompareTag("Obstacle"))
        {
            // Process as an obstacle collision, don't count as a gate pass
            GameController.Instance.UpdateHealth(damage); // Apply damage or any other collision effect
            audioManager.PlaySFX(audioManager.wallTouch); // Play collision sound effect
                                                          // Don't call GatePassed because it's an obstacle, not a gate
        }
    }
}


//if (other.CompareTag("Obstacle"))
//{

//    GameController.Instance.UpdateHealth(damage);
//    audioManager.PlaySFX(audioManager.wallTouch);
//}


