using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLoader : MonoBehaviour
{
    void Awake()
    {
        LevelManager.LoadLevelProgress();

        if (PlayerPrefs.HasKey("MusicVolume"))
            Options.volume = PlayerPrefs.GetFloat("MusicVolume");

        if (PlayerPrefs.HasKey("IsMusicMuted"))
            Options.isMusicMuted = (PlayerPrefs.GetInt("IsMusicMuted") == 1) ? true : false;
    }

}
