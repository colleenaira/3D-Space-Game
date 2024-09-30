using UnityEngine;


/*
 * Class: DevelopmentAudio
 * Description: Ensures essential components, AudioManager and GameController, are present in the scene.
 * Functionality: Checks if AudioManager and GameController instances exist at scene load. If not, instantiates them from prefabs. 
 * Use Case: Useful in development for modular testing and ensures essential managers are always active, regardless of the entry scene.
 */

public class DevelopmentAudio : MonoBehaviour
{
    public GameObject audioManagerPrefab;
    public GameObject gameControllerPrefab;

    // This will run before the Start method
    void Awake()
    {
        // Check if the instances of the managers are already in the scene
        if (FindObjectOfType<AudioManager>() == null && audioManagerPrefab != null)
        {
            Instantiate(audioManagerPrefab);
        }

        if (FindObjectOfType<GameController>() == null && gameControllerPrefab != null)
        {
            Instantiate(gameControllerPrefab);
        }
    }
}
