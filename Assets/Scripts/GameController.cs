using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Instantiated Objects")]
    [SerializeField] CheckColors checkColors;
    [SerializeField] GameObject mainImage;
    [SerializeField] GameObject congratulations;
    [SerializeField] TextMeshProUGUI colorsNotUsedText;
    [SerializeField] AudioSource completeSFX;
    [SerializeField] AudioSource incorrectSFX;
    [SerializeField] public GameObject[] gamePieces;

    [Header("Conditions")]
    [SerializeField] bool isGameComplete = false;
    [SerializeField] int numUniquePieces = 0;
    [SerializeField] int delayCongratulations = 1;
    [SerializeField] int delayIncorrect = 1;

    void Awake()
    {
        congratulations = GameObject.Find("Congratulations");
        colorsNotUsedText = FindObjectOfType<TextMeshProUGUI>();
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
                isGameComplete = false;
            }
            else
            {
                numUniquePieces++;
            }
        }

        if (!checkColors.allColorsUsed)
        {
            incorrectSFX.Play();
            colorsNotUsedText.gameObject.SetActive(true);
            Invoke("DeactivateColorsNotUsed", delayIncorrect);
        }
        
        // Checks if all game pieces are unique and all colors are being used.
        if (numUniquePieces == gamePieces.Length && checkColors.allColorsUsed)
        {
            Invoke("ActivateCongratulations", delayCongratulations);
            isGameComplete = true;
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

    void DeactivateColorsNotUsed()
    {
        colorsNotUsedText.gameObject.SetActive(false);
    }
}
