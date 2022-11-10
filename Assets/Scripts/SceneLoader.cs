/*
 * Script used to traverse the scenes saved in build settings.
 * 
 * Last update: 11/10/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [Tooltip("Only change if additional menu scenes added.")]
    [SerializeField] int sceneOffSet = 2; 
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1); // 1 = main menu scene.
    }

    public void QuitApplication()
    {
        Debug.Log("Closing application.");
        Application.Quit();
    }

    public void LoadSettings()
    {
        Debug.Log("Need to make new settings scene.");
    }

    public void LoadLevel(Button btn)
    {
        var level = btn.GetComponentInChildren<TMP_Text>().text;
        int sceneIndex = int.Parse(level) + sceneOffSet;
        if(sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            SceneManager.LoadScene("Place Holder");
        }
    }

    public void LoadNextLevel()
    {
        var currIndex = SceneManager.GetActiveScene().buildIndex;
        if(SceneManager.sceneCountInBuildSettings > currIndex + 1)
        {
            SceneManager.LoadScene(currIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Place Holder");
        }
    }
}
