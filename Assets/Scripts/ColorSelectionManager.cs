using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelectionManager : MonoBehaviour
{
    [Header("General")]
    public Color[] colorList;
    [SerializeField] Color curColor;
    [SerializeField] int colorCount;

    [Header("Sound Effects")]
    [SerializeField] AudioSource paintDropSFX;
    [SerializeField] AudioSource splashSFX;

    private void Awake()
    {
        AudioSource paintDropSFX = GameObject.Find("Paint Drop SFX").GetComponent<AudioSource>();
        AudioSource splashSFX = GameObject.Find("Splash SFX").GetComponent<AudioSource>();
        //AudioSource splashSFX = GameObject.Find("/Game Canvas/Tools Bar/Buttons/Splash SFX").GetComponent<AudioSource>();
    }
    void Update()
    {
        curColor = colorList[colorCount];

        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

        if (Input.GetMouseButtonDown(0))
        {
            if(hit.collider != null)
            {
                // Gets the sprite renderer components from the sprite selected.
                SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                sp.color = curColor;
                splashSFX.PlayOneShot(splashSFX.clip);
            }
        }
    }

    public void Paint(int colorCode)
    {
        colorCount = colorCode;
        paintDropSFX.Play();
    }
}
