using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    Color active = new  Color(1, 1, 1, 1);
    Color inActive = new Color(1, 1, 1, 0.7f);

    public Button on;
    public Button off;

    public bool MutedMusic;

    public void Start()
    {
        UpdateButtonsVisualState();
    }

    void UpdateButtonsVisualState()
    {
        on.GetComponent<Image>().color = !Options.isMusicMuted ? active : inActive;
        off.GetComponent<Image>().color = Options.isMusicMuted ? active : inActive;
    }

    public void MuteMusic()
    {
        AudioListener.pause = Options.isMusicMuted = MutedMusic;
        PlayerPrefs.SetInt("IsMusicMuted", (MutedMusic) ? 1 : 0);
        UpdateButtonsVisualState();
    }
}
