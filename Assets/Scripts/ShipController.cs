using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{

   
    public float FlySpeed = 5;
    public float YawSpeed = 100;
    public float RollSpeed = 60;
    public float PitchSpeed = 60;

    private float yaw;
    private float roll;
    private float pitch;
    private Rigidbody rb;

    public GameObject gameUI;
    public bool isGameStarted = false; // Added flag for game start

    // Variables to store input
    private float horizontalInput;
    private float verticalInput;
    private bool rollLeft;
    private bool rollRight;


    // Player Health 
    public GameController gameController;
    public float damage;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            isGameStarted = true;
            StartGame();
        }

        if (!isGameStarted) return;

        // Collect input in Update
        horizontalInput = Input.GetAxis("Horizontal"); // Left and Right
        verticalInput = -Input.GetAxis("Vertical"); // Up and Down
        rollLeft = Input.GetKey(KeyCode.Q);
        rollRight = Input.GetKey(KeyCode.E);
    }

    public void StartGame()
    {
        // Disable the main menu
        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }
        GameController.Instance.StartTimer();
    }

    void FixedUpdate()
    {
        // Don't proceed with the physics update
        if (!isGameStarted) return;

        rb.velocity = transform.forward * FlySpeed;


        // Apply movement in FixedUpdate
        float moveDistance = FlySpeed * Time.fixedDeltaTime;


        // Use the input to change yaw, pitch, and roll
        yaw += horizontalInput * YawSpeed * Time.fixedDeltaTime;
        pitch += verticalInput * PitchSpeed * Time.fixedDeltaTime;

        if (rollLeft)
        {
            roll += RollSpeed * Time.fixedDeltaTime;
        }
        else if (rollRight)
        {
            roll -= RollSpeed * Time.fixedDeltaTime;
        }

        roll = Mathf.Clamp(roll, -80, 80);

        // Adjust rotation based on the yaw, pitch, and roll
        rb.rotation = Quaternion.Euler(new Vector3(pitch, yaw, roll));

        // Move the ship forward based on the FlySpeed
        rb.MovePosition(rb.position + rb.transform.forward * moveDistance);


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameController.Instance.UpdateHealth(damage);
            audioManager.PlaySFX(audioManager.wallTouch);
            //Debug.Log("Collision with: " + collision.gameObject.name);
        }
    }


}

