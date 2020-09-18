using TMPro;
using UnityEngine;

public class TitleFontSize : MonoBehaviour
{
    public TextMeshProUGUI titleGoodSize;
    public TextMeshProUGUI[] titlesBadSize;

    private void Awake()
    {
        NormalizeFonts();
    }

    public void NormalizeFonts()
    {
        titleGoodSize.enableAutoSizing = true;
        titleGoodSize.ForceMeshUpdate();
        float fSize = titleGoodSize.fontSize;
        titleGoodSize.enableAutoSizing = false;
        foreach (TextMeshProUGUI title in titlesBadSize)
            title.fontSize = fSize;
    }

}
