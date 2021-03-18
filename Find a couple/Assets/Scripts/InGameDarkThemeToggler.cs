using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDarkThemeToggler : MonoBehaviour
{
    public GameObject fon;
    public GameObject darkFon;

    void Awake()
    {
        darkFon.gameObject.SetActive(Options.darkTheme);
        fon.gameObject.SetActive(!Options.darkTheme);
    }

}
