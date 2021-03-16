using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource AudioSource;
    private float musicVolume = 0.5f;
    void Start()
    {
        AudioSource.Play();
    }

   
    void Update()
    {
        AudioSource.volume = musicVolume;
    }

    public void updatesVolume(float volume)
    {
        musicVolume = volume;
    }
}
