/*
 * Script used to help adding new levels to Main menu easier.
 * 
 * Last update: 11/30/22
 */
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EditorLevelAssignment : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI text;
    void Start()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        
        string value = "";
        
        for(var i = 0; i< name.Length; i++)
        {
            if (char.IsDigit(name[i]))
            {
                value += name[i];
            }
        }

        text.text = value;
    }
}
