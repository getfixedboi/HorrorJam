using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource source;
    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        source.PlayOneShot(audioClip);
    }
    public void StopSound()
    {
        source.Stop();
    }
}
