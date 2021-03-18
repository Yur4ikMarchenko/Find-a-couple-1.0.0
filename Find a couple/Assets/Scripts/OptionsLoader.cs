using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLoader : MonoBehaviour
{
    public GameObject fon;
    public GameObject darkFon;
    void Awake()
    {
        LevelManager.LoadLevelProgress();

        if (PlayerPrefs.HasKey("MusicVolume"))
            Options.volume = PlayerPrefs.GetFloat("MusicVolume");

        if (PlayerPrefs.HasKey("IsMusicMuted"))
            Options.isMusicMuted = (PlayerPrefs.GetInt("IsMusicMuted") == 1) ? true : false;

        if (PlayerPrefs.HasKey("DarkTheme"))
        {
            Options.darkTheme = (PlayerPrefs.GetInt("DarkTheme") == 1) ? true : false;
            darkFon.gameObject.SetActive(Options.darkTheme);
            fon.gameObject.SetActive(!Options.darkTheme);
        }
    }

}
