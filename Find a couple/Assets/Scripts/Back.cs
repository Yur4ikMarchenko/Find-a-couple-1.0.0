using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void BacktoMenuu()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Menu");
    }
}
