using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunSound : MonoBehaviour
{
    private AudioSource audioSrc;
    public AudioClip[] weaponSounds;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    void playAudio(int index)
    {
        if (audioSrc)
        {
            audioSrc.PlayOneShot(weaponSounds[index]);
        }
    }
}
