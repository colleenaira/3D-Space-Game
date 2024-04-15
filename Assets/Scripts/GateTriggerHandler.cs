using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


// Tracks the number of gates 
// Notifies GameController to trigger end-game logic 

public class GateTriggerHandler : MonoBehaviour
{
    public static GateTriggerHandler Instance;

    public int gatesPassed = 0;
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
        Debug.Log("GatePassed called before increment: " + gatesPassed);
        gatesPassed++;
        Debug.Log("GatePassed called after increment: " + gatesPassed);

        if (gatesPassed >= totalGates)
        {
            Debug.Log("All gates passed!");
            OnAllGatesPassed?.Invoke();
            GameController.Instance.EndGame(); 
        }
        else
        {
            Debug.Log($"Remaining gates to pass: {totalGates - gatesPassed}");
        }
    }

    public void ResetGates()
    {
        gatesPassed = 0;
    }
}
