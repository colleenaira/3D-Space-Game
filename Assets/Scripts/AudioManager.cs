using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Class: AudioManager
 * Description: Manages and plays audio clips for various game events, maintaining a single instance throughout the game.
 * Usage: Attach this script to an AudioManager GameObject in the MainMenu scene.
 * Use Debug.Log statements for reviewing, identifying, and cleaning up any issues.
 */


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("------ Audio Source ------")]
    //[SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;   

    [Header("------ Audio Clips ------")]
    //public AudioClip background;
    public AudioClip wallTouch;
    public AudioClip checkPoint;
    public AudioClip gateIn;

    private AudioSource audioSource;    // Source component for playing sound effects

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        Debug.Log("AudioManager Awake is called.");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SFXSource = transform.Find("SFX").GetComponent<AudioSource>(); // Make sure this matches child GameObject name
            Debug.Log("AudioManager Instance set.");


        }
        else if (Instance != this)
        {
            Debug.Log("Another instance of AudioManager was found and destroyed.");
            Destroy(gameObject);
        }
    }

    /// Plays a one-shot sound effect using the specified audio clip.
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
  
}
