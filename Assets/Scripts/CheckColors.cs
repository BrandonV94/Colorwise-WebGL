using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class CheckColors : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberOfColorsUsedText;
    public bool allColorsUsed = false;

    [SerializeField] GameObject[] pieces;
    [SerializeField] Color[] colorsAvailble;
    [SerializeField] List<Color> colorsUsed;

    int numOfColorsAvailable = 0;
    int numOfColorsUsed = 0;
    

    void Awake()
    {
        numOfColorsAvailable = FindObjectOfType<ColorSelectionManager>().colorList.Length;
        numberOfColorsUsedText = this.GetComponent<TextMeshProUGUI>();
    }

    // Used a the Start method because awake was to fast to populate the variable
    private void Start()
    {
        pieces = FindObjectOfType<GameController>().gamePieces;
        colorsAvailble = FindObjectOfType<ColorSelectionManager>().colorList;
    }

    // Update is called once per frame
    void Update()
    {
        LookForColorsUsed();
        colorsUsed.Clear();
        numberOfColorsUsedText.text =  numOfColorsUsed + "/" + numOfColorsAvailable;
        checkIfAllColorsUsed();
    }

    void LookForColorsUsed()
    {
        foreach(var pieces in pieces)
        {
            Color color = pieces.GetComponent<SpriteRenderer>().color;
            if (colorsAvailble.Contains(color))
            {
                colorsUsed.Add(color);
            }
        }
        var distinctColorsUsed = colorsUsed.Distinct();
        numOfColorsUsed = distinctColorsUsed.Count();
    }

    bool checkIfAllColorsUsed()
    {
        if(numOfColorsUsed == numOfColorsAvailable)
        {
            return allColorsUsed = true;
        }
        return allColorsUsed = false;
    }
}
