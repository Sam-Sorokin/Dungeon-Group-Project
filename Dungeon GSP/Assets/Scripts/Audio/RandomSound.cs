using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public AudioClip[] audioClips;
    public bool playOnAwake = false;
    private AudioSource audioSource;

    // Update is called once per frame
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(playOnAwake)
        {
            playRandomSound();
        }
    }

    public void playRandomSound()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
