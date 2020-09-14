using System.Collections;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{

    private struct TextWithInitialFontSize
    {
        public TextMeshProUGUI text;
        public float initFontSize;

        public TextWithInitialFontSize(TextMeshProUGUI txt, float fs)
        {
            text = txt;
            initFontSize = fs;
        }
    }

    public RectTransform blackFadeWarning, quitButton, outline, holder; // outline parent de holder

    private Vector2 anchorMinOutline, anchorMaxOutline, anchorMinQuitButton, anchorMaxQuitButton;

    private TextWithInitialFontSize[] arrayTextsStruct;
    private bool running = false;

    private void Start()
    {
        anchorMinOutline = outline.anchorMin;
        anchorMaxOutline = outline.anchorMax;
        anchorMinQuitButton = quitButton.anchorMin;
        anchorMaxQuitButton = quitButton.anchorMax;
        FillArrayTextsStructAndFontSizeToZero();
        SetAnchorsToTheirMiddle();
    }

    private void FillArrayTextsStructAndFontSizeToZero()
    {
        int nbTexts = holder.childCount - 1; // -1 pour quitButton
        arrayTextsStruct = new TextWithInitialFontSize[nbTexts];
        for (int i = 0; i < nbTexts; i++)
        {
            TextMeshProUGUI text = holder.GetChild(i).GetComponent<TextMeshProUGUI>();
            text.ForceMeshUpdate();
            text.enableAutoSizing = false;
            arrayTextsStruct[i] = new TextWithInitialFontSize(text, text.fontSize);
            text.fontSize = 0;
        }
    }

    private void SetAnchorsToTheirMiddle()
    {
        outline.anchorMin = new Vector2((outline.anchorMin.x + outline.anchorMax.x) / 2, (outline.anchorMin.y + outline.anchorMax.y) / 2);
        outline.anchorMax = outline.anchorMin;
        quitButton.anchorMin = new Vector2((quitButton.anchorMin.x + quitButton.anchorMax.x) / 2, (quitButton.anchorMin.y + quitButton.anchorMax.y) / 2);
        quitButton.anchorMax = quitButton.anchorMin;
    }

    private IEnumerator MoveAnchorsTo(RectTransform toMove, Vector2 anchorsMin, Vector2 anchorsMax)
    {
        while (toMove.anchorMin != anchorsMin & toMove.anchorMax != anchorsMax)
        {
            toMove.anchorMin = Vector2.MoveTowards(toMove.anchorMin, anchorsMin, 1.2f * Time.unscaledDeltaTime);
            toMove.anchorMax = Vector2.MoveTowards(toMove.anchorMax, anchorsMax, 1.2f * Time.unscaledDeltaTime);
            yield return null;
        }
    }

    private IEnumerator ChangeFontSizeToInit(TextWithInitialFontSize t)
    {
        float speed = (t.initFontSize - t.text.fontSize) / 0.6f;
        while (t.text.fontSize != t.initFontSize)
        {
            t.text.fontSize = Mathf.MoveTowards(t.text.fontSize, t.initFontSize, speed * Time.unscaledDeltaTime);
            yield return null;
        }
    }

    public void ShowCredits()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(ShowAllCredits());
        }
    }

    private IEnumerator ShowAllCredits()
    {
        AudioManagerMenu.am.PlaySound("clickPause");
        blackFadeWarning.gameObject.SetActive(true);
        outline.gameObject.SetActive(true);
        yield return StartCoroutine(MoveAnchorsTo(outline, anchorMinOutline, anchorMaxOutline));

        foreach(TextWithInitialFontSize t in arrayTextsStruct)
        {
            StartCoroutine(ChangeFontSizeToInit(t));
            yield return new WaitForSecondsRealtime(0.25f);
        }

        yield return StartCoroutine(MoveAnchorsTo(quitButton, anchorMinQuitButton, anchorMaxQuitButton));
        running = false;
    }

    public void CloseCredits()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(CloseAllCredits());
        }
    }

    public IEnumerator CloseAllCredits()
    {
        AudioManagerMenu.am.PlaySound("Quit");
        blackFadeWarning.GetComponent<Animator>().Play("BlackFadeWarningOut");
        Vector2 newAnchorsQuitBtn = new Vector2((quitButton.anchorMin.x + quitButton.anchorMax.x) / 2, (quitButton.anchorMin.y + quitButton.anchorMax.y) / 2);
        yield return StartCoroutine(MoveAnchorsTo(quitButton, newAnchorsQuitBtn, newAnchorsQuitBtn));

        foreach (TextWithInitialFontSize t in arrayTextsStruct)
        {
            StartCoroutine(ChangeFontSizeToZero(t));
        }
        yield return new WaitForSecondsRealtime(0.4f);

        Vector2 newAnchorsOutline = new Vector2((outline.anchorMin.x + outline.anchorMax.x) / 2, (outline.anchorMin.y + outline.anchorMax.y) / 2);
        yield return StartCoroutine(MoveAnchorsTo(outline, newAnchorsOutline, newAnchorsOutline));
        running = false;
        blackFadeWarning.gameObject.SetActive(false);
    }

    private IEnumerator ChangeFontSizeToZero(TextWithInitialFontSize t)
    {
        while (t.text.fontSize != 0)
        {
            t.text.fontSize = Mathf.MoveTowards(t.text.fontSize, 0, 200 * Time.unscaledDeltaTime);
            yield return null;
        }
    }

}
