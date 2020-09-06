using TMPro;
using UnityEngine;

public class TitleFontSize : MonoBehaviour
{
    public TextMeshProUGUI titleGoodSize;
    public TextMeshProUGUI[] titlesBadSize;

    void Start()
    {
        float fSize = titleGoodSize.fontSize;
        foreach (TextMeshProUGUI title in titlesBadSize)
            title.fontSize = fSize;
    }

}
