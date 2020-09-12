using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractMenu : MonoBehaviour
{
    public static bool Paused { get; protected set; } = false;

    public Image blackFadeQuitEnter;
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
            AudioManagerMenu.am.PlaySound("clickPause");
            Pause();
            if (pauseButton)
                pauseButton.SetActive(false);
            StartCoroutine(MoveToActivePosWithFadeIn());
        }
    }

    public void DeactivateMenu()
    {
        if (!moving)
        {
            AudioManagerMenu.am.PlaySound("clickResume");
            StartCoroutine(MoveToNotActivePosWithFadeOut());
        }
    }

    public static void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
    }

    public static void Unpause()
    {
        Time.timeScale = 1;
        Paused = false;
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
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 0.85f - (currentDistance / maxDistance) * 0.85f);
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
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 0.85f * (currentDistance / maxDistance));
            // move right
            Vector2 newPos = Vector2.MoveTowards(rect.anchoredPosition, notActivePos, moveSpeed * 2f);
            rect.anchoredPosition = newPos;
            yield return null;
        }
        fader.color = new Color(0, 0, 0, 0);
        moving = false;
        Unpause();
        if (pauseButton)
            pauseButton.SetActive(true);
    }

    protected void ToMainMenu()
    {
        AudioManagerMenu.am.PlaySound("Quit");
        //AudioSource source = System.Array.Find(AudioManagerMenu.am.sounds, sound => sound.name == "Quit").source;
        //AudioSource s = gameObject.AddComponent<AudioSource>();
        //s.clip = source.clip;
        //s.volume = source.volume;
        //s.pitch = source.pitch;
        //s.Play();
        LoadSceneUtility.LoadLevelAsyncWithFade(blackFadeQuitEnter, 0);
    }

}
