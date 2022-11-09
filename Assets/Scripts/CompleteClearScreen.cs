/*
 * Activated once the game has completed. The art supplies moves off the screen and 
 * the curtains and station move in to provide a gallery aesthetic.
 * 
 * Last update: 11/9/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteClearScreen : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject artEasel;
    [SerializeField] GameObject artPalette;
    [SerializeField] GameObject colorsUsedText;
    [SerializeField] GameObject pictureFrame;
    [SerializeField] GameObject redCurtains;
    [SerializeField] GameObject ropeStantion;
    [SerializeField] GameObject congratulations;

    [Header("Timers")]
    [SerializeField] float transition = 1f;
    [SerializeField] float delayArtSuppliesMovement = 1f;
    [SerializeField] float delayCompletedStageMovement = 1f;

    [Header("Conditions")]
    [SerializeField] public bool activateMovement = false;

    private void Awake()
    {
        InstantiateObjects();
    }

    void Update()
    {
        MoveGameObjects(activateMovement);
    }

    void MoveGameObjects(bool activate)
    {
        if (activate)
        {
            activateMovement = false;
            LeanTween.moveX(pictureFrame, 0.02630782f, transition); 
            LeanTween.moveY(colorsUsedText, 7f, transition); 
            Invoke("ClearArtSupplies", delayArtSuppliesMovement);
            Invoke("SetupCompletedStage", delayCompletedStageMovement);
        }
    }

    void ClearArtSupplies()
    {
        LeanTween.moveY(artEasel, -10f, transition); 
        LeanTween.moveY(artPalette, -8f, transition); 
        Destroy(pictureFrame, delayArtSuppliesMovement);
    }

    void SetupCompletedStage()
    {
        LeanTween.moveY(redCurtains, 0f, transition); 
        LeanTween.moveY(ropeStantion, -4.34f, transition); 
        LeanTween.moveX(congratulations, 0, transition);
    }

    void InstantiateObjects()
    {
        artEasel = GameObject.Find("Art Easel");
        if(artEasel == null) { Debug.Log("Art Easel missing!"); }

        artPalette = GameObject.Find("Art Palette");
        if(artPalette == null) { Debug.Log("Art Palette missing!"); }

        colorsUsedText = GameObject.Find("Colors Used Text (TMP)");
        if (colorsUsedText == null) { Debug.Log("Colors Used Text missing!"); }

        pictureFrame = GameObject.Find("Picture Frame Transparent");
        if (pictureFrame == null) { Debug.Log("Picture Frame missing!"); }

        redCurtains = GameObject.Find("Red Curtains");
        if (redCurtains == null) { Debug.Log("Red Curtains missing!"); }

        ropeStantion = GameObject.Find("Rope Stantion");
        if (ropeStantion == null) { Debug.Log("Rope Stantion missing!"); }

        congratulations = GameObject.Find("Congratulations Banner");
        if (congratulations == null) { Debug.Log("Congratulations Banner missing!"); }
    }
}
