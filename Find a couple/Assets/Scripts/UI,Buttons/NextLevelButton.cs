using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().interactable = !LevelManager.IsLastLevel();
    }

    public void LoadNextLevel()
    {
        LevelManager.SetLevel(LevelManager.currentLevel + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
