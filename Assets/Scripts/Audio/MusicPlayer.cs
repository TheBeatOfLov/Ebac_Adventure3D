using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource audioSource;

    private MusicSetup _currentSetup;

    private void Start()
    {
        Play();
    }

    private void Play()
    {
        _currentSetup = AudioManager.Instance.GetMusicByType(musicType);

        audioSource.clip = _currentSetup.audioClip;
        audioSource.Play();
    }
}
