/*
 * Script used to assign all buttons in level select menu to the
 * SceneLoader method LoadLevel().
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonAssignment : MonoBehaviour
{
    void Start()
    {
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        var buttons = FindObjectsOfType<Button>();

        foreach(var btn in buttons)
        {
            btn.onClick.AddListener(delegate ()
            {
                sceneLoader.LoadLevel(btn);
            });
        }
    }
}
