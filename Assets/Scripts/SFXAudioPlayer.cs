using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource sfxAudioSource;

    private void Awake()
    {
        sfxAudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        sfxAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    } 
}
