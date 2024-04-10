using UnityEngine;

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
