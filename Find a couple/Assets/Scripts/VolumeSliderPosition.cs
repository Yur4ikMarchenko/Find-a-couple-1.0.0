using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderPosition : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = Options.volume;
    }

}
