using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLeanTweenScript : MonoBehaviour
{
    [SerializeField] float duration = 1f;

    // Component List
    [SerializeField] GameObject uiArtEasel;
    [SerializeField] GameObject uiArtPalete;
    [SerializeField] GameObject uiColorsUsedText;
    //[SerializeField] GameObject uiCongratulationsBanner;

    [SerializeField] bool activateMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveComponents(activateMovement);
    }

    void MoveComponents(bool activate)
    {
        if (activate)
        {
            activateMovement = false;
            LeanTween.moveX(gameObject, 0.02630782f, duration); // Move the frame over easel.
            LeanTween.moveY(uiArtEasel, -10f, duration); // Move to bottom of screen.
            LeanTween.moveY(uiArtPalete, -8f, duration); // Move to bottom of screen.
            LeanTween.moveY(uiColorsUsedText, 7f, duration); // Move to top of screen.
        }
    }
}
