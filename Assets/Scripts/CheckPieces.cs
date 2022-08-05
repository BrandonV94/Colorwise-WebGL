using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPieces : MonoBehaviour
{
    [SerializeField] Component[] adjacentSprites;
    [SerializeField] Color curColor;


    public bool isPieceUnique = false;
    string hexWhite = "FFFFFF";


    void Update()
    {
        curColor = gameObject.GetComponent<SpriteRenderer>().color;
        CheckPieceIsUnique();
    }

    void CheckPieceIsUnique()
    {
        if(ColorUtility.ToHtmlStringRGB(curColor) != hexWhite && SurroundingsUnique(adjacentSprites))
        {
            isPieceUnique = true;
        }
        else
        {
            isPieceUnique = false;
        }
    }

    bool SurroundingsUnique(Component[] pieces)
    {
        foreach (var piece in pieces)
        {
            if(piece.GetComponent<SpriteRenderer>().color == curColor &&
                ColorUtility.ToHtmlStringRGB(piece.GetComponent<SpriteRenderer>().color) != hexWhite)
            {
                return false;
            }
        }
        return true;
    }
}
