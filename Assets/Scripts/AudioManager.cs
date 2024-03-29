using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------ Audio Clips ------")]
    public AudioClip background;
    public AudioClip wallTouch;
    public AudioClip checkPoint;
    public AudioClip gateIn;


    // Start is called before the first frame update
    private AudioSource audioSource;

    void Start()
    {
        // Start BGM
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
