/*
 *  Main script in charge of gameplay.
 *  Task:
 *  Make any specific components inactive im the hierarchy. 
 *  Check for game complete conditions.
 *  Activate any signs and banners based on specific conditions.
 *  Toggles the sign on the Check Button based on specific conditions.
 *  
 *  Last Update: 11/29/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject checkBtn;
    [SerializeField] GameObject btnCheck;
    [SerializeField] GameObject btnCross;
    [SerializeField] GameObject mainImage;
    [SerializeField] GameObject completeAssets;
    [SerializeField] public GameObject[] gamePieces;

    [Header("Text and Banners")]
    [SerializeField] GameObject congratulations;
    [SerializeField] TextMeshProUGUI allColorsNotUsedText;

    [Header("Sound Effects")]
    [SerializeField] AudioSource completeSFX;
    [SerializeField] AudioSource incorrectSFX;

    [Header("Conditions")]
    [SerializeField] int numUniquePieces = 0;
    [SerializeField] int delayStageComplete = 1;
    [SerializeField] int delayIncorrect = 1;
    [SerializeField] bool isPulsing = false;
    [SerializeField] public bool isGameOver = false;

    [Header("Scripts")]
    [SerializeField] CheckColors checkColors;
    [SerializeField] CompleteClearScreen completeClearScreen;


    void Awake()
    {
        CheckInstantiatedGameObjects();
    }

    
    private void Start()
    {
        DeactivateGameObjectsOnStart();
    }

    // Checks all game pieces and determines if the game is complete.
    public void CheckGameOver()
    {
        // Checks game pieces for any unique pieces.
        foreach (var piece in gamePieces)
        {
            var pieceSR = piece.GetComponent<SpriteRenderer>();
            var unique = piece.GetComponent<CheckPieces>();
            if (unique.isPieceUnique == false)
            {
                incorrectSFX.Play();
                isPulsing = true;
                StartCoroutine(Pulse(pieceSR));
            }
            else
            {
                numUniquePieces++;
            }
        }

        if (!checkColors.allColorsUsed)
        {
            incorrectSFX.Play();
            ActivateColorNotUsed();
        }
        
        // Checks if all game pieces are unique and all colors are being used.
        if (numUniquePieces == gamePieces.Length && checkColors.allColorsUsed)
        {
            completeSFX.Play();

            ActivateCongratulations();
            Invoke("BeginGameCompleteSequence", delayStageComplete);
        }

        numUniquePieces = 0;
    }

    // Makes any non-unique pieces pulse when checking for game results.
    IEnumerator Pulse(SpriteRenderer pieceSR)
    {
        toggleCheckButton();
        int countDown = 12;
        for (; countDown > 0; countDown--)
        {
            if (pieceSR.sortingOrder == 0)
            {
                pieceSR.sortingOrder = 5;
            }
            else
            {
                pieceSR.sortingOrder = 0;
            }
            yield return new WaitForSeconds(.25f);
        }

        pieceSR.sortingOrder = 0;
        isPulsing = false;
        toggleCheckButton();
        yield break;    // Breaks the coroutine after flashing for a few seconds
    }

    void BeginGameCompleteSequence()
    {
        completeClearScreen.activateMovement = true;
    }

    // Brings up the congratulation banner and plays complete jingle.
    void ActivateCongratulations()
    {
        isGameOver = true;

        checkBtn.SetActive(false);
        completeAssets.gameObject.SetActive(true);
        congratulations.gameObject.SetActive(true);
    }

    // Brings up the Colors Not Used game object and plays the incorrect SFX.
    void ActivateColorNotUsed()
    {
        allColorsNotUsedText.gameObject.SetActive(true);
        Invoke("DeactivateColorsNotUsed", delayIncorrect);
    }

    // Called in ActivateColorNotUsed() to remove text.
    void DeactivateColorsNotUsed()
    {
        allColorsNotUsedText.gameObject.SetActive(false);
    }

    // Toggles the Check Button's Check and Cross sign. 
    void toggleCheckButton()
    {
        if(isPulsing)
        {
            checkBtn.GetComponent<Image>().color = new Color(255,0,0);
            btnCheck.gameObject.SetActive(false);
            btnCross.gameObject.SetActive(true);
        }
        else
        {
            checkBtn.GetComponent<Image>().color = Color.green;
            btnCheck.gameObject.SetActive(true);
            btnCross.gameObject.SetActive(false);
        }
    }

    // Sets specific game objects to inactive on game start.
    void DeactivateGameObjectsOnStart()
    {
        congratulations.gameObject.SetActive(false);
        allColorsNotUsedText.gameObject.SetActive(false);
        btnCross.gameObject.SetActive(false);

        completeAssets.gameObject.SetActive(false);
    }

    // Sets game objects to variables and verifies nothing is missing.
    void CheckInstantiatedGameObjects()
    {
        // Locates game objects and sets them to appropriate variables.
        checkBtn = GameObject.Find("Check Button");
        btnCheck = GameObject.Find("Check Sprite");
        btnCross = GameObject.Find("Cross Sprite");
        congratulations = GameObject.Find("Congratulations Banner");
        allColorsNotUsedText = GameObject.Find("All Colors Not Used Text (TMP)")
            .GetComponent<TextMeshProUGUI>();
        mainImage = GameObject.Find("Main");
        gamePieces = GameObject.FindGameObjectsWithTag("PlayablePiece");
        completeAssets = GameObject.Find("Complete Assets");

        // Find game SFX
        completeSFX = GameObject.FindGameObjectWithTag("CompleteSFX")
            .GetComponent<AudioSource>();
        incorrectSFX = GameObject.FindGameObjectWithTag("IncorrectSFX")
            .GetComponent<AudioSource>();

        // Find related scripts
        checkColors = FindObjectOfType<CheckColors>();
        completeClearScreen = FindObjectOfType<CompleteClearScreen>();

        // Checks all components are available. 
        if (checkBtn == null) { Debug.LogError("Missing Check Button game object."); }
        if (btnCheck == null) { Debug.LogError("Missing Check Sprite game object."); }
        if (btnCross == null) { Debug.LogError("Missing Cross Sprite game object."); }
        if (congratulations == null) 
            { Debug.LogError("Missing Congratulations Banner game object."); }
        if (allColorsNotUsedText == null)
            { Debug.LogError("Missing All Colors Not Used Text (TMP) game object."); }
        if (mainImage == null) { Debug.LogError("Missing Main game object."); }
        if (gamePieces.Length < 1) { Debug.LogError("No pieces found! Check game pieces tag."); }
        if (checkColors == null) { Debug.LogError("Missing Check Colors script."); }
    }
}
