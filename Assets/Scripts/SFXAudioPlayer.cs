/*
 * Script used to control the audio for the SFX Audio Source attached.
 * 
 * Last update: 12/8/2022
 */
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

    public void PlayAudio()
    {
        // TODO FIgure out how to play audio once.
        Debug.Log("Playing audio clip.");
        sfxAudioSource.PlayOneShot(sfxAudioSource.clip);
    }
}
