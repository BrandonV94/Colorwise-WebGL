/*
 * Script used to assign all buttons in level select menu to the
 * SceneLoader method LoadLevel().
 * 
 * Last update: 11/30/22
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
            // Makes sure Settings Button isn't assigned a level.
            if (btn.name == "Settings Button")
                return;

            btn.onClick.AddListener(delegate ()
            {
                sceneLoader.LoadLevel(btn);
            });
        }
    }
}
