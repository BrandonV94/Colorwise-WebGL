/*
 * Script used to control the in game volume settings. 
 * Script recieves inital settings and values from the PlayerPrefsController.
 * 
 * Last update: 11/22/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // Script
    PlayerPrefsController playerPrefsController;

    [Header("Settings Buttons")]
    [SerializeField] Button saveBtn;
    [SerializeField] Button muteBtn;

    [Header("Volume Setings")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] const float DEFAULT_MUSIC_VOLUME = .1f;
    [SerializeField] const float DEFAULT_SFX_VOLUME = 1f;

    [Header("Audio Components")]
    [SerializeField] MusicAudioPlayer musicPlayer;
    [SerializeField] SFXAudioPlayer splashSFX;
    [SerializeField] SFXAudioPlayer paintDropSFX;
    [SerializeField] SFXAudioPlayer incorrectSFX;
    [SerializeField] SFXAudioPlayer completeSFX;

    [Header("Conditions")]
    public bool isGamePaused = false;

    // Canvas
   GameObject settingsCanvas;

    void Awake()
    {
        playerPrefsController = FindObjectOfType<PlayerPrefsController>();
        settingsCanvas = GameObject.FindGameObjectWithTag("Settings");
        FindAllAudioPlayers();

        musicSlider = GameObject.FindGameObjectWithTag("MusicSlider")
            .GetComponent<Slider>();

        sfxSlider = GameObject.FindGameObjectWithTag("SFXSlider")
            .GetComponent<Slider>();

        saveBtn = GameObject.Find("Save Button").GetComponent<Button>();
        muteBtn = GameObject.Find("Mute All Button").GetComponent<Button>();
    }

    private void OnEnable()
    {
        isGamePaused = true;
    }

    private void Start()
    {
        AssignSettingButtons();
    }

    void Update()
    {
        if (sfxSlider && musicSlider)
        {
            musicPlayer.SetMusicVolume(musicSlider.value);
            splashSFX.SetSFXVolume(sfxSlider.value);
            paintDropSFX.SetSFXVolume(sfxSlider.value);
            incorrectSFX.SetSFXVolume(sfxSlider.value);
            completeSFX.SetSFXVolume(sfxSlider.value);
        }
    }

    private void OnDisable()
    {
        isGamePaused = false;
    }

    public void PlaySFXSound()
    {
        paintDropSFX.PlayAudio();
    }

    // Saves the current volume to PlayerPrefsController. 
    public void SaveAndReturn()
    {
        // Saves volume settings to PlayerPrefsController
        PlayerPrefsController.SetMasterMusicVolume(musicSlider.value);
        PlayerPrefsController.SetMasterSFXVolume(sfxSlider.value);

        // Deactivates Settings canvas and moves up.
        // Up canvas pos: X = -0.13921 , Y = 11
        LeanTween.moveY(settingsCanvas, 11f, 1f);

        // Delay deactivation
        Invoke("DeactivateSettings", 1);
    }

    // Method that will be connected to a button to automatically mute all sounds.
    public void SetAllVolumeToDefaults()
    {
        musicPlayer.SetMusicVolume(DEFAULT_MUSIC_VOLUME);
        splashSFX.SetSFXVolume(DEFAULT_SFX_VOLUME);
        paintDropSFX.SetSFXVolume(DEFAULT_SFX_VOLUME);
        incorrectSFX.SetSFXVolume(DEFAULT_SFX_VOLUME);
        completeSFX.SetSFXVolume(DEFAULT_SFX_VOLUME);
    }

    public void MuteAll()
    {
        musicPlayer.SetMusicVolume(0);
        splashSFX.SetSFXVolume(0);
        paintDropSFX.SetSFXVolume(0);
        incorrectSFX.SetSFXVolume(0);
        completeSFX.SetSFXVolume(0);
        musicSlider.value = 0;
        sfxSlider.value = 0;
    }

    void DeactivateSettings()
    {
        settingsCanvas.gameObject.SetActive(false);
    }

    void FindAllAudioPlayers()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("Music")
            .GetComponent<MusicAudioPlayer>();

        splashSFX = GameObject.FindGameObjectWithTag("SplashSFX")
            .GetComponent<SFXAudioPlayer>();

        paintDropSFX = GameObject.FindGameObjectWithTag("PaintDropSFX")
            .GetComponent<SFXAudioPlayer>();

        incorrectSFX = GameObject.FindGameObjectWithTag("IncorrectSFX")
            .GetComponent<SFXAudioPlayer>();

        completeSFX = GameObject.FindGameObjectWithTag("CompleteSFX")
            .GetComponent<SFXAudioPlayer>();
    }

    private void AssignSettingButtons()
    {
        musicSlider.value = PlayerPrefsController.GetMasterMusicVolume();
        sfxSlider.value = PlayerPrefsController.GetMasterSFXVolume();

        // Assign Buttons
        saveBtn.onClick.AddListener(delegate ()
        {
            SaveAndReturn();
        });

        muteBtn.onClick.AddListener(delegate ()
        {
            SetAllVolumeToDefaults();
        });
    }
}
