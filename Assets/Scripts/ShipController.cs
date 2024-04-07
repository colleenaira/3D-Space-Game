using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //public float impactForce = 10f; // The force applied to the ship upon collision
    //public float controlRecoveryTime = 2f; // Time in seconds before control is fully regained
    //public float moveSpeed = 7f;
    //public float turnSpeed = 60f;

    public float FlySpeed = 7f; 
    public float YawSpeed = 100;
    public float RollSpeed = 60;
    public float PitchSpeed = 60;

    private float yaw;
    private float roll;
    private float pitch;
    private Rigidbody rb;

    public GameObject gameUI;
    public bool isGameStarted = false; // Added flag for game start

    private float horizontalInput;
    private float verticalInput;
    private bool rollLeft;
    private bool rollRight;
    public Transform thisShip;


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
        rb.useGravity = false;
    }

    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))         // Press spacebar to Start
        {
            StartGame();
        }

        if (!isGameStarted) return;         //Collect input in Update

        horizontalInput = Input.GetAxis("Horizontal");  // Left and Right
        verticalInput = Input.GetAxis("Vertical");      // Up and Down
        rollLeft = Input.GetKey(KeyCode.Q);
        rollRight = Input.GetKey(KeyCode.E);
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
        // Don't proceed with the physics update
        if (!isGameStarted) return;
        //Turn();
        //Thrust();

        rb.velocity = transform.forward * FlySpeed;


        // Apply movement in FixedUpdate
        float moveDistance = FlySpeed * Time.fixedDeltaTime;

        //roll = Mathf.Clamp(roll + rollChange, -80, 80);       //Clamp the roll within the specified limits

        //Use the input to change yaw, pitch, and roll
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

        rb.rotation = Quaternion.Euler(new Vector3(pitch, yaw, roll));         // Adjust rotation based on the yaw, pitch, and roll

        rb.MovePosition(rb.position + rb.transform.forward * moveDistance);    // Move the ship forward based on the FlySpeed

    }
    //void Turn()
    //{
    //    // Gather input for yaw (turn), pitch (nose up/down), and roll (tilt)
    //    float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
    //    float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Vertical");
    //    float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Rotate");

    //    // Rotate the ship based on input
    //    thisShip.Rotate(pitch, yaw, roll);
    //}

    //void Thrust()
    //{
    //    // Move the ship forward continuously in the direction it's facing
    //    rb.velocity = transform.forward * moveSpeed;
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //rb.AddForce(-collision.contacts[0].normal * impactForce, ForceMode.Impulse);
            //StartCoroutine(FreezeRotationTemporarily());

            GameController.Instance.UpdateHealth(damage);
            //Debug.Log("Collision with: " + collision.gameObject.name);
            audioManager.PlaySFX(audioManager.wallTouch);
        }

    }


}

