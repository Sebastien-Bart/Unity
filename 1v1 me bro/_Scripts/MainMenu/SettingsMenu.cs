using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : AbstractMenu
{
    public TextMeshProUGUI musicBtnTxt, soundBtnTxt, helpMenusBtnTxt, cameraShakeBtnTxt;

    public Image curFlagImg;
    public Sprite flagFR, flagUK;
    public TextMeshProUGUI langAbbr;

    protected override void Start()
    {
        base.Start();
        if (PlayerPrefs.GetInt("music", 1) == 0)
            musicBtnTxt.text = "X";
        if (PlayerPrefs.GetInt("sound", 1) == 0)
            soundBtnTxt.text = "X";
        if (PlayerPrefs.GetInt("helpMenus", 1) == 0)
            helpMenusBtnTxt.text = "X";
        if (PlayerPrefs.GetInt("cameraShake", 1) == 0)
            cameraShakeBtnTxt.text = "X";
        if (PlayerPrefs.GetString("lang", "en") == "fr")
        {
            curFlagImg.sprite = flagFR;
            langAbbr.text = "fr";
        }

        notActivePos = new Vector2(rect.rect.width, 0);
        rect.anchoredPosition = notActivePos;
    }

    protected override IEnumerator MoveToActivePosWithFadeIn()
    {
        Time.timeScale = 1;
        fader.gameObject.SetActive(true);
        yield return StartCoroutine(base.MoveToActivePosWithFadeIn());
    }

    protected override IEnumerator MoveToNotActivePosWithFadeOut()
    {
        yield return StartCoroutine(base.MoveToNotActivePosWithFadeOut());
        fader.gameObject.SetActive(false);
    }

    public void OnClickDisableOption(string keyPlayerPref)
    {
        TextMeshProUGUI btnTxt = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.GetInt(keyPlayerPref, 1) == 1)
        {
            PlayerPrefs.SetInt(keyPlayerPref, 0);
            btnTxt.text = "X";
        }
        else
        {
            PlayerPrefs.SetInt(keyPlayerPref, 1);
            btnTxt.text = "";
        }
        if (keyPlayerPref == "music")
            Array.Find(AudioManagerForOneGame.am.sounds, sound => sound.name == "MainTheme").source.volume = PlayerPrefs.GetInt(keyPlayerPref);
    }

    public void ChangeLanguageSettings()
    {
        string l;
        if (PlayerPrefs.GetString("lang", "en") == "en")
        {
            PlayerPrefs.SetString("lang", "fr");
            curFlagImg.sprite = flagFR;
            l = "fr";
        }
        else
        {
            PlayerPrefs.SetString("lang", "en");
            curFlagImg.sprite = flagUK;
            l = "en";
        }
        langAbbr.text = l;
        UpdateAllTextToLanguage(l);
    }

    private void UpdateAllTextToLanguage(string l)
    {
        TextToLanguage[] toChange = FindObjectsOfType<TextToLanguage>();
        foreach (TextToLanguage t in toChange)
            t.ChangeLanguage(l);
        GetComponent<TitleFontSize>().NormalizeFonts();
    }
    
}
