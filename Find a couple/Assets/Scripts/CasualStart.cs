using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CasualStart : MonoBehaviour
{
    public Text text;
    public void StartCasualLevel()
    {
        Options.volume = GameObject.FindObjectOfType<AudioSource>().volume;
        LevelManager.casual = true;
        LevelManager.pairs = Convert.ToInt32(text.text);
        SceneManager.LoadScene("Game");
    }
}
