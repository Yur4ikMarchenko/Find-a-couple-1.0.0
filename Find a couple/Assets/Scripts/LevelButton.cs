using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public void StartLevel()
    {
        Options.volume = GameObject.FindObjectOfType<AudioSource>().volume;
        LevelManager.SetLevel(int.Parse(name.Substring(3)) - 1);
        SceneManager.LoadScene("Game");
    }

    private void Awake()
    {
        GetComponent<Button>().interactable = LevelManager.IsLevelAvailable(int.Parse(name.Substring(3)) - 1);
    }

    private void Update()
    {
        GetComponent<Button>().interactable = LevelManager.IsLevelAvailable(int.Parse(name.Substring(3)) - 1);
    }
}
