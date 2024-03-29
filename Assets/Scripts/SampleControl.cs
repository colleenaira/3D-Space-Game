using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input for pitch and yaw based on the ship's local axes.
        float pitch = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        float yaw = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float roll = Input.GetAxis("Roll") * rotationSpeed * Time.deltaTime; // Assuming you have an input set up for "Roll"

        // Apply rotation.
        transform.Rotate(pitch, yaw, roll, Space.Self);
    }

    void FixedUpdate()
    {
        // Move the ship forward continuously in local space.
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }


}
