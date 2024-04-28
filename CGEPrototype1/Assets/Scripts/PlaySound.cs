using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// require the AudioSource component to be added to the game object
[RequireComponent(typeof(AudioSource))]

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundToPlay;

    // add slider ranging from 0 to 1 in unity
    [Range(0,1)]
    public float volume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(soundToPlay, volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
