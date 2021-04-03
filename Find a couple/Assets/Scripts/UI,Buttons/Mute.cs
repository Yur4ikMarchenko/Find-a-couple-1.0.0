using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    public Text on;
    public Text off;

    public bool MutedMusic;

    public void Start()
    {
        UpdateButtonsVisualState();
    }

    void UpdateButtonsVisualState()
    {
        on.GetComponent<Text>().gameObject.SetActive(!Options.isMusicMuted);
        off.GetComponent<Text>().gameObject.SetActive(Options.isMusicMuted);
    }

    public void MuteMusic()
    {
        AudioListener.pause = Options.isMusicMuted = MutedMusic;
        PlayerPrefs.SetInt("IsMusicMuted", (MutedMusic) ? 1 : 0);
        UpdateButtonsVisualState();
    }
}
