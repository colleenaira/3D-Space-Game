using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SFXSource = transform.Find("SFX").GetComponent<AudioSource>(); // Make sure this matches your child GameObject name

            // Initialize your audio sources here
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
  
}
