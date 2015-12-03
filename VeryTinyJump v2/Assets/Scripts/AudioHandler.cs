using UnityEngine;
/* Audio Handler class, implements Singleton pattern */
public class AudioHandler : MonoBehaviour
{

    private static AudioHandler instance;
    private AudioSource ac;

    public static AudioHandler Instance
    {
        get { return instance ?? (instance = new GameObject("AudioHandler").AddComponent<AudioHandler>()); }
    }

    /* Called by Instance.PlayAudio. From everywhere. Singleton rocks */
    public void PlayAudio(AudioSource src)
    {
        ac = src;
        ac.Play();
    }
}