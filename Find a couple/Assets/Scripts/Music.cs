using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource AudioSource;


    private static Music instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(AudioSource.gameObject);
    }

    void Start()
    {
        AudioSource.Play();
    }

   
    void Update()
    {
        AudioSource.volume = Options.volume;
    }


}
