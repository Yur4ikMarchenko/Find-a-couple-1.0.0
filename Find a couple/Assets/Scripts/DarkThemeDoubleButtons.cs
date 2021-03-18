using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkThemeDoubleButtons : MonoBehaviour
{
    public bool darkTheme;
    public GameObject fon;
    public GameObject darkFon;
    public Text on;
    public Text off;

    private void Start()
    {
        UpdateState();
    }

    public void ChangeDoubleButtonState()
    {
        Options.darkTheme = darkTheme;
        PlayerPrefs.SetInt("DarkTheme", (darkTheme) ? 1 : 0);
        UpdateState();
    }

    private void UpdateState()
    {
        on.gameObject.SetActive(Options.darkTheme);
        darkFon.gameObject.SetActive(Options.darkTheme);

        off.gameObject.SetActive(!Options.darkTheme);
        fon.gameObject.SetActive(!Options.darkTheme);
    }
}
