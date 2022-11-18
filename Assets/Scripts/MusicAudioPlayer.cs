using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource;

    private void Awake()
    {
        musicAudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        musicAudioSource.volume = PlayerPrefsController.GetMasterMusicVolume();
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    } 
}
