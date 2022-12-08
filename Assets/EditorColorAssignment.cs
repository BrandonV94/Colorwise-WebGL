/*
 * Get the buttons color from the ColorSelectionManager script.
 */
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EditorColorAssignment : MonoBehaviour
{
    [SerializeField] ColorSelectionManager colorSelectionManager;
    [SerializeField] Image btnColor;
    [SerializeField] int colorElement = 0;

    private void Start()
    {
        colorSelectionManager = GetComponentInParent<ColorSelectionManager>();
        btnColor = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        btnColor.color = colorSelectionManager.colorList[colorElement];
    }
}
