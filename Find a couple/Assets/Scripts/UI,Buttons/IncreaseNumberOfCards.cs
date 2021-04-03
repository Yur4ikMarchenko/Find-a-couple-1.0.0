using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseNumberOfCards : MonoBehaviour
{
    public Text text;

    public void Increase()
    {
        if (Convert.ToInt32(text.text) < 52)
            text.text = (Convert.ToInt32(text.text) + 1).ToString();
    }
}
