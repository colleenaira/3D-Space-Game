using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detects when ship passes through a gate 
// Notifies GateTriggerHandler 


public class GateTrigger : MonoBehaviour
{
    // Declare a public static event that other scripts can subscribe to.
    public static event System.Action OnGatePassed;
    private bool hasBeenTriggered = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTriggered && other.CompareTag("Ship"))
        {
            audioManager.PlaySFX(audioManager.gateIn);
            OnGatePassed?.Invoke();
            hasBeenTriggered = true; // Set the flag to true to prevent re-triggering

            //Debug.Log($"Gate triggered by {other.gameObject.name}");
            //// If the GameObject this script is attached to is tagged as "Gate"
            //if (gameObject.CompareTag("Gate"))
            //{
            //    OnGatePassed?.Invoke(); // This line raises the event
            //    hasBeenTriggered = true;
            //    audioManager.PlaySFX(audioManager.gateIn);
            //}
           
        }
    }
}



//    private void OnTriggerEnter(Collider other)
//    {
//        // When the ship enters the trigger and the gate hasn't been passed yet...
//        if (!hasBeenTriggered && other.CompareTag("Ship"))
//        {
//            audioManager.PlaySFX(audioManager.gateIn);
//            OnGatePassed?.Invoke();
//            hasBeenTriggered = true; // Set the flag to true to prevent re-triggering

//            // Invoke the OnGatePassed event to notify all subscribers.
          

//        }
//    }
//}

//private void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("Ship"))
//    {
//        // Invoke the OnGatePassed event to count as a pass regardless
//        OnGatePassed?.Invoke();

//        if (gameObject.CompareTag("Wall"))
//        {
//            // This means the ship hit the wall part of the gate
//            Debug.Log("The ship collided with the wall.");
//            // Here you can add logic to count it as a collision for scoring or feedback
//            // You might want to store this info or invoke another event for wall collisions
//        }
//        else if (gameObject.CompareTag("Gate"))
//        {
//            Debug.Log("The ship passed through the cutout.");
//            // Additional logic for successful cutout pass, if needed
//        }

//        audioManager.PlaySFX(audioManager.gateIn);
//    }
//}