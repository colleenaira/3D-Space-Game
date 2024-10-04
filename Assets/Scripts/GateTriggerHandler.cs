using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


/*
 * Class: GateTriggerHandler
 * Description: Tracks the number of gates. Notifies GameController to trigger end-game logic when all gates are passed.
 * Usage: Attach this script to the GameManager GameObject in the MainMenu scene. 
 * Use Debug.Log statements for reviewing, identifying, and cleaning up any issues. 
*/


public class GateTriggerHandler : MonoBehaviour
{
    public static GateTriggerHandler Instance;

    public int gatesPassed = 0;
    public int totalGates = 4; // Set this in the inspector or calculate it at runtime
    public event Action OnAllGatesPassed;


    void Awake()
    {
        //Debug.Log($"Awake called on {gameObject.name}, Instance is null: {Instance == null}");

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


    // This method subscribes to gate pass events 
    private void OnEnable()
    {
        GateTrigger.OnGatePassed += GatePassed;
    }

    // This method unsubscribes to gate pass events 
    private void OnDisable()
    {
        GateTrigger.OnGatePassed -= GatePassed;
    }


    // This method increments the gate pass count and checks for game progression
    public void GatePassed()
    {
        //Debug.Log("GatePassed called before increment: " + gatesPassed);
        gatesPassed++;
        //Debug.Log("GatePassed called after increment: " + gatesPassed);

        if (gatesPassed >= totalGates)
        {
            Debug.Log("All gates passed!");
            //OnAllGatesPassed?.Invoke();
            GameController.Instance.EndGame(); 
        }
        else
        {
            //Debug.Log($"Remaining gates to pass: {totalGates - gatesPassed}");
        }
    }


    // This method resets the count of gates passed after each scene
    public void ResetGates()
    {
        gatesPassed = 0;
    }
}
