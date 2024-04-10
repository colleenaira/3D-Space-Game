using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    public Transform thisShip;

    public Rigidbody r;

    public float turnSpeed = 60f;
    public float moveSpeed = 45f;



    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        r.useGravity = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Turn();
        Thrust();
    }

    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Rotate");
        thisShip.Rotate(pitch, yaw, roll);

    }

    void Thrust()
    {
        r.velocity = transform.forward * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Handle what happens when hitting an asteroid
            Debug.Log("Passed through an asteroid!");
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log($"Collision with {collision.gameObject.name}: point {collision.contacts[0].point}, normal {collision.contacts[0].normal}, velocity {r.velocity}");
    //    // Check for collision with a "Rock" tag
    //    if (collision.gameObject.CompareTag("Rock"))
    //    {
    //        // Apply a force in the opposite direction of the collision
    //        r.AddForce(-collision.contacts[0].normal * impactForce, ForceMode.Impulse);

    //        // Optionally freeze rotation temporarily to help the player regain control
    //        StartCoroutine(FreezeRotationTemporarily());
    //    }
    //}

    //IEnumerator FreezeRotationTemporarily()
    //{
    //    var originalAngularDrag = r.angularDrag;
    //    r.angularDrag = 100; // Temporarily increase angular drag to "freeze" rotation
    //    yield return new WaitForSeconds(controlRecoveryTime);
    //    r.angularDrag = originalAngularDrag; // Restore original angular drag
    //}

}




