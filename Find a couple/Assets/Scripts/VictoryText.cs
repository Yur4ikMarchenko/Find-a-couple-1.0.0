using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Text>().text = (LevelManager.IsLastLevel()) ? "You've completed the game!" : "Nice!";
    }
}
