using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introAudio, loopAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        introAudio.Play();
        loopAudio.PlayScheduled(AudioSettings.dspTime + introAudio.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
