using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Class: GateTrigger
 * Description: Detects when the ship passes through a gate and notifies the GateTriggerHandler script. 
 * Use Debug.Log statements for reviewing, identifying, and cleaning up any issues. 
 */


public class GateTrigger : MonoBehaviour
{
    public static event System.Action OnGatePassed;     // Declare a public static event that other scripts can subscribe to.
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
            hasBeenTriggered = true;    // Set the flag to true to prevent re-triggering
            audioManager.PlaySFX(audioManager.gateIn);
            OnGatePassed?.Invoke();
        }
    }
}
