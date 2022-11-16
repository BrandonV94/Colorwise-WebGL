using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    PlayerPrefsController playerPrefsController;

    [Header("Volume Setings")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] const float DEFAULT_MUSIC_VOLUME = .1f;
    [SerializeField] const float DEFAULT_SFX_VOLUME = 1f;

    [Header("Audio Components")]
    [SerializeField] AudioPlayer musicPlayer;
    [SerializeField] AudioPlayer splashSFX;
    [SerializeField] AudioPlayer paintDropSFX;
    [SerializeField] AudioPlayer incorrectSFX;
    [SerializeField] AudioPlayer completeSFX;


    void Awake()
    {
        playerPrefsController = FindObjectOfType<PlayerPrefsController>();
        FindAllAudioPlayers();

        musicSlider = GameObject.Find("Music Volume/Music Slider").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFX Volume/SFX Slider").GetComponent<Slider>();
    }


    void Update()
    {
        if (sfxSlider && musicSlider)
        {
            musicPlayer.SetVolume(musicSlider.value);
            splashSFX.SetVolume(sfxSlider.value);
            paintDropSFX.SetVolume(sfxSlider.value);
            incorrectSFX.SetVolume(sfxSlider.value);
            completeSFX.SetVolume(sfxSlider.value);
        }
    }

    void FindAllAudioPlayers()
    {
        musicPlayer = GameObject.Find("Important GameObject/Music Player")
            .GetComponent<AudioPlayer>();

        splashSFX = GameObject.Find("Important GameObject/SFX Game Objects/Splash SFX")
            .GetComponent<AudioPlayer>();

        paintDropSFX = GameObject.Find("Important GameObject/SFX Game Objects/Paint Drop SFX")
            .GetComponent<AudioPlayer>();

        incorrectSFX = GameObject.Find("Important GameObject/SFX Game Objects/Incorrect SFX")
            .GetComponent<AudioPlayer>();

        completeSFX = GameObject.Find("Important GameObject/SFX Game Objects/Complete SFX")
            .GetComponent<AudioPlayer>();
    }

    void SetAllVolumeToDefaults()
    {
        musicPlayer.SetVolume(DEFAULT_MUSIC_VOLUME);
        splashSFX.SetVolume(DEFAULT_SFX_VOLUME);
        paintDropSFX.SetVolume(DEFAULT_SFX_VOLUME);
        incorrectSFX.SetVolume(DEFAULT_SFX_VOLUME);
        completeSFX.SetVolume(DEFAULT_SFX_VOLUME);
    }

}
