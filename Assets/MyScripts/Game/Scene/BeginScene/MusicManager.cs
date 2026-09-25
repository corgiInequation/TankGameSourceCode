using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public static MusicManager Instance => instance;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = !DataMg.Instance.musicData.musicOpen;
        audioSource.volume = DataMg.Instance.musicData.musicSize;
    }

    public void OnAudio()
    {
        audioSource.mute = false;
    }
    public void OffAudio()
    {
        audioSource.mute = true;
    }
    public void ChangeSize(float size)
    {
        audioSource.volume = size;
    }
    
}
