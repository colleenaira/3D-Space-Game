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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // When the ship enters the trigger and the gate hasn't been passed yet...
        if (!hasBeenTriggered && other.CompareTag("Ship"))
        {
            audioManager.PlaySFX(audioManager.gateIn);
            OnGatePassed?.Invoke();
            hasBeenTriggered = true; // Set the flag to true to prevent re-triggering

            // Invoke the OnGatePassed event to notify all subscribers.
          

        }
    }
} 