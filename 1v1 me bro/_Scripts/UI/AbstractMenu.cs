using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractMenu : MonoBehaviour
{
    public static bool paused { get; private set; } = false;

    public Image fader;
    public float moveSpeed = 15f;

    public GameObject pauseButton;

    protected Vector2 activPos;
    protected Vector2 notActivePos;
    protected RectTransform rect;
    protected bool moving = false;

    protected virtual void Start()
    {
        rect = GetComponent<RectTransform>();
        notActivePos = rect.anchoredPosition;
        activPos = new Vector2(0, 0);
    }

    public virtual void ActivateMenu()
    {
        if (!moving)
        {
            if(pauseButton)
                pauseButton.SetActive(false);
            Pause();
            StartCoroutine(MoveToActivePosWithFadeIn());
        }
    }

    public void DeactivateMenu()
    {
        if (!moving)
        {
            StartCoroutine(MoveToNotActivePosWithFadeOut());
        }
    }

    protected void Pause()
    {
        Time.timeScale = 0;
        paused = true;
    }

    protected void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
    }

    protected virtual IEnumerator MoveToActivePosWithFadeIn()
    {
        moving = true;
        float maxDistance = Vector2.Distance(rect.anchoredPosition, activPos);
        float currentDistance;
        while (rect.anchoredPosition != activPos)
        {
            // fade in
            currentDistance = Vector2.Distance(rect.anchoredPosition, activPos);
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 0.75f - (currentDistance / maxDistance) * 0.75f);
            // move
            Vector2 newPos = Vector2.MoveTowards(rect.anchoredPosition, activPos, moveSpeed);
            rect.anchoredPosition = newPos;
            yield return null;
        }
        moving = false;
    }

    protected virtual IEnumerator MoveToNotActivePosWithFadeOut()
    {
        moving = true;
        float maxDistance = Vector2.Distance(rect.anchoredPosition, notActivePos);
        float currentDistance;
        while (rect.anchoredPosition != notActivePos)
        {
            // fade out
            currentDistance = Vector2.Distance(rect.anchoredPosition, notActivePos);
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 0.75f * (currentDistance / maxDistance));
            // move right
            Vector2 newPos = Vector2.MoveTowards(rect.anchoredPosition, notActivePos, moveSpeed * 4f);
            rect.anchoredPosition = newPos;
            yield return null;
        }
        fader.color = new Color(0, 0, 0, 0);
        moving = false;
        Unpause();
        if (pauseButton)
            pauseButton.SetActive(true);
    }

}
