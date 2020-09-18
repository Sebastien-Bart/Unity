using System;
using TMPro;
using UnityEngine;

public class TextToLanguage : MonoBehaviour
{
    public string textFR;
    private string textEN;

    private TextMeshProUGUI textPro;

    private void Awake()
    {
        textPro = GetComponent<TextMeshProUGUI>();
        textFR = textFR.Replace("\\n", "\n");
        textEN = textPro.text;
        ChangeLanguage(PlayerPrefs.GetString("lang", "en"));
    }

    public void ChangeLanguage(string l)
    {
        if (l == "fr")
            textPro.text = textFR;
        else
            textPro.text = textEN;
    }

}
