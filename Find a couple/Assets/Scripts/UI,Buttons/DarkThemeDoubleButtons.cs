using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkThemeDoubleButtons : MonoBehaviour
{
    public bool darkTheme;
    public GameObject fon;
    public GameObject darkFon;
    public Button on;
    public Button off;

    Color active = new Color(1, 1, 1, 1);
    Color inActive = new Color(1, 1, 1, 0.7f);

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
        on.GetComponent<Image>().color = Options.darkTheme ? active : inActive;
        off.GetComponent<Image>().color = !Options.darkTheme ? active : inActive;

        darkFon.gameObject.SetActive(Options.darkTheme);
        fon.gameObject.SetActive(!Options.darkTheme);
    }
}
