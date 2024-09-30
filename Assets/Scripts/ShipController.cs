using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


/*
 * Class: ShipController
 * Description: Controls the player's ship, handling movement, collision detection, and gameplay interaction.
 * Used to handle timer and health UI, health changes upon collisions
 * Usage: Attached to the Player Model Ship GameObject in the scene. 
 * Use Debug.Log statements for reviewing, identifying, and cleaning up any issues.
 */


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

    private int collisionCount = 0;
    public float collisionTime = 0f;   
    private bool inCollision = false;

    private void Awake()
    {
        //Debug.Log("ShipController: Awake");
        audioManager = AudioManager.Instance; // Instead of using GameObject.FindGameObjectWithTag
    }

    void Start()
    {
        //Debug.Log("ShipController: Start");
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        //Debug.Log("ShipController: Update running");
        
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space)) // Press spacebar to Start
        {
            //Debug.Log("Spacebar pressed, calling StartGame.");
            StartGame();
        }

        if (!isGameStarted) return;         //Collect input in Update

        if (inCollision)
        {
            collisionTime += Time.deltaTime;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to the sceneLoaded event
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe to the sceneLoaded event
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        collisionCount = 0;  // Reset collision count when a new scene is loaded
        //Debug.Log("Scene loaded, collision count reset.");
    }


    public void StartGame()
    {
        //Debug.Log("ShipController: StartGame");
        
        isGameStarted = true;
        gameUI.SetActive(false);  // Disables the main menu
        GameController.Instance.SetGameStarted(true);
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
        if (other.CompareTag("Obstacle") && !inCollision)
        {
            collisionCount++;
            inCollision = true;
            audioManager.PlaySFX(audioManager.wallTouch);
            
            //Debug.Log("Collision with obstacle: " + other.name + " at " + Time.time);
            Debug.Log("New Collision Counted: " + collisionCount);
        }
    }

    public int GetCollisionCount()
    {
        return collisionCount;  // Method to access the collision count
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            inCollision = false;
            //Debug.Log("Exited collision with: " + other.name);

        }
    }

  
}



