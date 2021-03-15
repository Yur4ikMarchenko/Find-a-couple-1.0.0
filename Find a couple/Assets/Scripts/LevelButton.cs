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
        GetComponent<Button>().interactable = LevelManager.IsLevelAvailable(int.Parse(GetComponentInChildren<Text>().text.Substring(5)) - 1);
    }

    private void Update()
    {
        Debug.Log(LevelManager.IsLevelAvailable(1));
        GetComponent<Button>().interactable = LevelManager.IsLevelAvailable(int.Parse(GetComponentInChildren<Text>().text.Substring(5)) - 1);
    }
}
