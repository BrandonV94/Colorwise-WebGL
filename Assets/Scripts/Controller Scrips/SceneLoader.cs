/*
 * Script used to traverse the scenes saved in build settings.
 * 
 * Last update: 12/7/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Setings")]
    [Tooltip("Only change if additional menu scenes added.")]
    [SerializeField] int sceneOffSet = 2;

    [Header("Button Controls")]
    [Tooltip("Duration in seconds")]
    [SerializeField] float duration = 2;

    // Settings Canvas
    GameObject settingCanvas;


    private void Start()
    {
        // Prevents error from occuring in these specific scenes.
        if(SceneManager.GetActiveScene().name == "Settings" ||
            SceneManager.GetActiveScene().name == "Place Holder" ||
            SceneManager.GetActiveScene().name == "Main Menu" ||
            SceneManager.GetActiveScene().name == "Start Menu")
        {
            return;
        }
        else
        {
            settingCanvas = GameObject.FindGameObjectWithTag("Settings");
            settingCanvas.gameObject.SetActive(false);
        }   
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1); // 1 = Main Menu scene.
    }

    public void LoadSettings()
    {
        /*
         * Coordinates for middle of screen 
         * X = -0.13921 Y = 0.13361
         */
        settingCanvas.gameObject.SetActive(true);
        LeanTween.moveY(settingCanvas, 0.013361f, 1f);
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

    public void SettingsScene()
    {
        SceneManager.LoadScene(2); // 2 = Settings scene.
    }

    public void StartGame()
    {
        var startBtn = GameObject.Find("Start Button")
            .GetComponentInChildren<TextMeshProUGUI>();
        startBtn.color = new Color32(51, 140, 36, 255); // Green #338C24
        var bgImage = GameObject.FindGameObjectWithTag("Background");
        LeanTween.alpha(bgImage, 1f, duration);
        Invoke("LoadMainMenu", duration);
    }

    public void QuitApplication()
    {
        var quitBtn = GameObject.Find("Quit Button")
            .GetComponentInChildren<TextMeshProUGUI>();
        quitBtn.color = new Color32(180, 20, 20, 255); // Red #B41414
        Debug.Log("Closing application.");
        Application.Quit();
    }
}
