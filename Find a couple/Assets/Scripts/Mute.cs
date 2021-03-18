using UnityEngine;

public class Mute : MonoBehaviour
{
    public bool MutedMusic = true;
    public void MuteMusic()
    {
        AudioListener.pause = MutedMusic;
    }
}
