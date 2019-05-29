using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance = null;
    public AudioClip Rotate;
    public AudioClip Strike;
    public AudioClip Delete;
    public AudioClip GameOver;

    private AudioSource SoundEffectAudio;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        AudioSource theSource = GetComponent<AudioSource>();

        SoundEffectAudio = theSource;
    }

    public void PlayOneShot(AudioClip clip)
    {
        SoundEffectAudio.PlayOneShot(clip);
    }
}
