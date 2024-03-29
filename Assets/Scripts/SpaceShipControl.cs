using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipControl : MonoBehaviour
{
    [Header("===Speed movement===")]
    [SerializeField]
    float speedMult = 1;
    [SerializeField]
    float speedMultAngle = 0.5f;
    [SerializeField]
    float speedRollMultAngle = 0.05f;

    Rigidbody spaceshipRB;
    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;


    private float gameStartTime;
    public float inputStartDelay = 1.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        spaceshipRB = GetComponent<Rigidbody>();
        gameStartTime = Time.time; // Capture the start time

    }

    // Start is called before the first frame update
    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");

        if (Time.time - gameStartTime > inputStartDelay)
        {
            mouseInputX = Input.GetAxis("Mouse X");
            mouseInputY = Input.GetAxis("Mouse Y");
        }
        else
        {
            // Optionally reset inputs to 0 during the delay
            mouseInputX = 0;
            mouseInputY = 0;
        }
  
    }

    private void FixedUpdate()
    {
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.right) * horizontalMove * speedMult, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.right * speedMultAngle * mouseInputY * -1 , ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);


    }
}

