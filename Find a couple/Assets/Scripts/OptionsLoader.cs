using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLoader : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            Options.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
