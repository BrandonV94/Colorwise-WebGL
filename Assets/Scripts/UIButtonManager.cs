/*
 * Script used to assign UI buttons OnClick function for each level automatically.
 * 
 * Last update: 11/10/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] GameObject menuBtn;
    [SerializeField] GameObject settingBtn;
    [SerializeField] GameObject checkBtn;
    [SerializeField] GameObject nextLevelBtn;

    // Scripts
    [SerializeField] GameController gameController;
    [SerializeField] SceneLoader sceneLoader;

    void Awake()
    {
        // Buttons
        menuBtn = GameObject.Find("Menu Button");
        if (menuBtn == null) { Debug.LogError("Menu Button missing!"); }

        settingBtn = GameObject.Find("Settings Button");
        if(settingBtn == null) { Debug.LogError("Setting Button missing!"); }

        checkBtn = GameObject.Find("Check Button");
        if(checkBtn == null) { Debug.LogError("CHeck Button missing!"); }

        nextLevelBtn = GameObject.Find("Next Level Button");
        if(nextLevelBtn == null) { Debug.LogError("Next Level Button missing!"); }
        // Scripts
        gameController = FindObjectOfType<GameController>();
        if( gameController == null) { Debug.LogError("GameController script missing!"); }

        sceneLoader = FindObjectOfType<SceneLoader>();
        if(sceneLoader == null) { Debug.LogError("SceneLoader script missing!"); }
    }

    void Start()
    {
        menuBtn.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            sceneLoader.LoadMainMenu();
        });

        settingBtn.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            sceneLoader.LoadSettings();
        });

        checkBtn.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            gameController.CheckGameOver();
        });

        nextLevelBtn.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            sceneLoader.LoadNextLevel();
        });
    }
}
