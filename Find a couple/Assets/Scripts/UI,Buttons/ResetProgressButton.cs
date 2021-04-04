using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgressButton : MonoBehaviour
{
    Color active = new Color(1, 1, 1, 1);
    Color inActive = new Color(1, 1, 1, 0.7f);

    private void Start()
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        GetComponent<Image>().color = LevelManager.IsLevelAvailable(1) ? active : inActive;
    }

    public void ResetProgress()
    {
        LevelManager.ResetProgress();
        UpdateVisual();
    }
}
