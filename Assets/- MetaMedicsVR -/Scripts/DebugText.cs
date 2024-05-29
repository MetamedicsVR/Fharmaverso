using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    public TextMeshProUGUI canvasText;
    private string[] debugLines = new string[20];

    public void Log(string message)
    {
        for (int i = debugLines.Length - 1; i > 0; i--)
        {
            debugLines[i] = debugLines[i - 1];
        }
        debugLines[0] = message;
        string allLines = "";
        for (int i = 0; i < debugLines.Length; i++)
        {
            allLines += debugLines[i] + "\r\n";
        }
        canvasText.text = allLines;
    }
}
