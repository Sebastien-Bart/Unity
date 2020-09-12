using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SettingsMenu : AbstractMenu
{
    public TextMeshProUGUI musicBtnTxt, soundBtnTxt, helpMenusBtnTxt, cameraShakeBtnTxt;

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
    
}
