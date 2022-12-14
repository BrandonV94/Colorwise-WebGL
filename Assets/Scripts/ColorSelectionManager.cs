/*
 * The Color Selection Manager is a script used to select different colors in game 
 * and allow users to change the color of a game piece that has a collider component.
 * The script is also destroyed when the game has concluded and the completion screen 
 * is activated.
 * 
 * Last update: 11/7/2022 
 */
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectionManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] int colorCount;
    [SerializeField] Color curColor;
    public Color[] colorList;

    [Header("Sound Effects")]
    [SerializeField] AudioSource paintDropSFX;
    [SerializeField] AudioSource splashSFX;

    // Scripts
    GameController gameController;
    SettingsController settingsController;

    private void Awake()
    {
        paintDropSFX = GameObject.FindGameObjectWithTag("PaintDropSFX")
            .GetComponent<AudioSource>();

        splashSFX = GameObject.FindGameObjectWithTag("SplashSFX")
            .GetComponent<AudioSource>();

        gameController = FindObjectOfType<GameController>();
        settingsController = FindObjectOfType<SettingsController>();
    }

    private void Start()
    {
        SetButtonColors();
    }

    void Update()
    {
        if (settingsController.isGamePaused)
        {
            return;
        }
        else
        {
            curColor = colorList[colorCount];

            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider != null)
                {
                    // Gets the sprite renderer components from the sprite selected.
                    SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    sp.color = curColor;
                    splashSFX.PlayOneShot(splashSFX.clip);
                }
            }
        }

        // Stops users from being able to paint after game completion.
        if(gameController.isGameOver) { Destroy(gameObject); }
    }

    public void Paint(int colorCode)
    {
        colorCount = colorCode;
        paintDropSFX.Play();
    }

    // TODO Remove this method and start method.
    void SetButtonColors()
    {
        var btn1 = GameObject.Find("Button_1").GetComponent<Image>();
        btn1.color = colorList[0];
        var btn2 = GameObject.Find("Button_2").GetComponent<Image>();
        btn1.color = colorList[0];
        var btn3 = GameObject.Find("Button_3").GetComponent<Image>();
        btn1.color = colorList[0];
    }
}
