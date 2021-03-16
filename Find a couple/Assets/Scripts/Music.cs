using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource AudioSource;
    private float musicVolume = Options.volume;
    void Start()
    {
        Debug.Log(Options.musicTime);

        AudioSource.time = Options.musicTime;
        AudioSource.Play();
    }

   
    void Update()
    {
        Options.musicTime = AudioSource.time;
        AudioSource.volume = musicVolume;
    }

    public void updatesVolume(float volume)
    {
        musicVolume = Options.volume = volume;
    }
}
