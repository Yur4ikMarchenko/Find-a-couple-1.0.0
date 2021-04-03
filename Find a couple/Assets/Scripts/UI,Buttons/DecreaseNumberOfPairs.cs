using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreaseNumberOfPairs : MonoBehaviour
{
    public Text text;

    public void Decrease()
    {
        if (Convert.ToInt32(text.text) > 1)
            text.text = (Convert.ToInt32(text.text) - 1).ToString();
    }
}
