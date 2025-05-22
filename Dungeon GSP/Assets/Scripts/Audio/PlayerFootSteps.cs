using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }
    public void PlayFootStepSound()
    {
        Debug.Log("Play FootStep");
        playerAudio.Play();

        //float rndnumb = Random.Range(0.4f, 0.6f);
        //playerAudio.volume = rndnumb;

        //rndnumb = Random.Range(0.8f, 1.3f);
        //playerAudio.pitch = rndnumb;
    }

    public void StopFootStepSound()
    {
        playerAudio.Stop();
    }
    // Update is called once per frame
}
