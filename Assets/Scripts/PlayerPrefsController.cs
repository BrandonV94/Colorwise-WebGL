/*
 * Script used to access Player Pref settings and save volume settings for music and SFX.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_MUSIC_VOLUME_KEY = "master music volume";
    const string MASTER_SFX_VOLUME_KEY = "master sfx volume";
    const string DEFAULT_MASTER_VOLUME_KEY = "default volume";


    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    #region Music volume control methods 
    public static void SetMasterMusicVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_MUSIC_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Music volume is out of range.");
        }
    }

    public static float GetMasterMusicVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_MUSIC_VOLUME_KEY);
    }
    #endregion

    #region SFX volume control methods
    public static void SetMasterSFXVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_SFX_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("SFX volume is out of range.");
        }
    }

    public static float GetMasterSFXVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_SFX_VOLUME_KEY);
    }
    #endregion
}