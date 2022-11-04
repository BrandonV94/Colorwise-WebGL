using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject checkButton;
    [SerializeField] GameObject buttonsCheck;
    [SerializeField] GameObject buttonsCross;
    [SerializeField] CheckColors checkColors;
    [SerializeField] GameObject mainImage;
    [SerializeField] public GameObject[] gamePieces;
    
    [Header("Text and Banners")]
    [SerializeField] GameObject congratulations;
    [SerializeField] TextMeshProUGUI allColorsNotUsedText;

    [Header("Sound Effects")]
    [SerializeField] AudioSource completeSFX;
    [SerializeField] AudioSource incorrectSFX;

    [Header("Conditions")]
    [SerializeField] int numUniquePieces = 0;
    [SerializeField] int delayCongratulations = 1;
    [SerializeField] int delayIncorrect = 1;
    [SerializeField] bool isPulsing = false;

    
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
            ActivateColorNotUsed();
        }
        
        // Checks if all game pieces are unique and all colors are being used.
        if (numUniquePieces == gamePieces.Length && checkColors.allColorsUsed)
        {
            Invoke("ActivateCongratulations", delayCongratulations);
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

    // Brings up the congratulation banner and plays complete jingle.
    void ActivateCongratulations()
    {
        completeSFX.Play();
        congratulations.gameObject.SetActive(true);
    }

    // Brings up the Colors Not Used game object and plays the incorrect SFX.
    void ActivateColorNotUsed()
    {
        incorrectSFX.Play();
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
            checkButton.GetComponent<Image>().color = new Color(255,0,0);
            buttonsCheck.gameObject.SetActive(false);
            buttonsCross.gameObject.SetActive(true);
        }
        else
        {
            checkButton.GetComponent<Image>().color = Color.green;
            buttonsCheck.gameObject.SetActive(true);
            buttonsCross.gameObject.SetActive(false);
        }
    }

    // Sets game objects to variables and verifies nothing is missing.
    void CheckInstantiatedGameObjects()
    {
        // Locates game objects and sets them to appropriate variables.
        checkButton = GameObject.Find("Check Button");
        buttonsCheck = GameObject.Find("Check");
        buttonsCross = GameObject.Find("Cross");
        congratulations = GameObject.Find("Congratulations Banner");
        allColorsNotUsedText = GameObject.Find("All Colors Not Used Text (TMP)")
            .GetComponent<TextMeshProUGUI>();
        mainImage = GameObject.Find("Main");
        gamePieces = GameObject.FindGameObjectsWithTag("PlayablePiece");
        checkColors = FindObjectOfType<CheckColors>();
        
        // Checks all components are available. 
        if (checkButton == null) { Debug.Log("Missing Check Button game object.");}
        if (buttonsCheck == null) { Debug.Log("Missing Check game object."); }
        if (buttonsCross == null) { Debug.Log("Missing Cross  game object."); }
        if (congratulations == null) { Debug.Log("Missing Congratulations Banner game object."); }
        if (allColorsNotUsedText == null) 
        { Debug.Log("Missing All Colors Not Used Text (TMP) game object."); }
        if (mainImage == null) { Debug.Log("Missing Main game object."); }
        if (gamePieces.Length < 1) { Debug.Log("No pieces found! Check game pieces tag."); }
        if (checkColors == null) { Debug.Log("Missing Check Button game object."); }
    }

    // Sets specific game objects to inactive.
    void DeactivateGameObjectsOnStart()
    {
        congratulations.gameObject.SetActive(false);
        allColorsNotUsedText.gameObject.SetActive(false);
        buttonsCross.gameObject.SetActive(false);
    }
}
