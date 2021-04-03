using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressButton : MonoBehaviour
{
    public void ResetProgress()
    {
        LevelManager.ResetProgress();
    }
}
