using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


// Tracks the number of gates 
// Notifies GameController to trigger end-game logic 

public class GateTriggerHandler : MonoBehaviour
{
    public static GateTriggerHandler Instance;

    private int gatesPassed = 0;
    public int totalGates = 4; // Set this in the inspector or calculate it at runtime
    public event Action OnAllGatesPassed;


    void Awake()
    {
        Debug.Log($"Awake called on {gameObject.name}, Instance is null: {Instance == null}");
        // Singleton pattern setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GateTrigger.OnGatePassed += GatePassed;
    }

    private void OnDisable()
    {
        GateTrigger.OnGatePassed -= GatePassed;
    }


    public void GatePassed()
    {
        gatesPassed++;
        Debug.Log($"Gate passed. Current count: {gatesPassed}");

        if (gatesPassed >= totalGates)
        {
            // Trigger end of level sequence or show the UI for level completion
            Debug.Log("All gates passed!");

            // Call your method here that handles end of level
            OnAllGatesPassed?.Invoke();
        }
        else
        {
            Debug.Log($"Remaining gates to pass: {totalGates - gatesPassed}");
        }
    }


}
