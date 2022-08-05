using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Instantiated Objects")]
    [SerializeField] CheckColors checkColors;
    [SerializeField] GameObject mainImage;
    [SerializeField] public GameObject[] gamePieces;

    [Header("Text and Banners")]
    [SerializeField] GameObject congratulations;
    [SerializeField] TextMeshProUGUI colorsNotUsedText;

    [Header("Sound Effects")]
    [SerializeField] AudioSource completeSFX;
    [SerializeField] AudioSource incorrectSFX;

    [Header("Conditions")]
    [SerializeField] int numUniquePieces = 0;
    [SerializeField] int delayCongratulations = 1;
    [SerializeField] int delayIncorrect = 1;

    void Awake()
    {
        congratulations = GameObject.Find("Congratulations");
        colorsNotUsedText = GameObject.Find("All Colors Not Used Text (TMP)")
            .GetComponent<TextMeshProUGUI>();
        mainImage = GameObject.Find("Main");
        gamePieces = GameObject.FindGameObjectsWithTag("PlayablePiece");
        checkColors = FindObjectOfType<CheckColors>();
    }

    private void Start()
    {
        congratulations.gameObject.SetActive(false);
        colorsNotUsedText.gameObject.SetActive(false);
    }

    public void CheckGameOver()
    {
        // Checks for any unique pieces.
        foreach (var piece in gamePieces)
        {
            var pieceSR = piece.GetComponent<SpriteRenderer>();
            var unique = piece.GetComponent<CheckPieces>();
            if (unique.isPieceUnique == false)
            {
                incorrectSFX.Play();
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

    // Makes any non-unique piece pulse when checking for game results.
    IEnumerator Pulse(SpriteRenderer pieceSR)
    {
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
        yield break;    // Breaks the coroutine after flashing for a few seconds
    }

    // Brings up the congratulation banner and plays compete jingle.
    void ActivateCongratulations()
    {
        completeSFX.Play();
        congratulations.gameObject.SetActive(true);
    }

    void ActivateColorNotUsed()
    {
        incorrectSFX.Play();
        colorsNotUsedText.gameObject.SetActive(true);
        Invoke("DeactivateColorsNotUsed", delayIncorrect);
    }

    void DeactivateColorsNotUsed()
    {
        colorsNotUsedText.gameObject.SetActive(false);
    }
}
