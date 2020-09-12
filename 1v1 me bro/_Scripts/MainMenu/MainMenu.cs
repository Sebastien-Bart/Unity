using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Header("TMP REMPLACER PAR L'ACHAT")]
    public bool fullAccess;

    public WarningBrocoin warningBrocoin;

    public Image blackFadeQuitEnter;
    public GameObject blocker;
    public Image[] grayFades;
    public Transform[] playButtons;

    public float maxTimeCounter;
    public float minTimeCounter;

    /* 
    Build Index / idxGameOfDay :
    1 = Cow-boys
    2 = Pikafruit
    3 = Pong
    4 = Stars war
    */

    IEnumerator Start()
    {
        blackFadeQuitEnter.gameObject.SetActive(true);
        while (!SplashScreen.isFinished)
        {
            yield return null;
        }
        LoadSceneUtility.FadeOnLevelLoaded(blackFadeQuitEnter);
        if (!fullAccess) // PlayerPrefs.GetInt("fullAccess", -1) == 1
        {
            foreach (Image grayfade in grayFades)
                grayfade.color = new Color(grayfade.color.r, grayfade.color.g, grayfade.color.b, 0.75f);
            foreach (Transform playButton in playButtons)
            {
                playButton.GetChild(0).gameObject.SetActive(false); //txt
                playButton.GetChild(1).gameObject.SetActive(true); //brocoin img
            }

            int today = DateTime.Now.Day;
            if (today != PlayerPrefs.GetInt("today", -1))
            {
                PlayerPrefs.SetInt("today", today);
                blocker.SetActive(true);
                StartCoroutine(ChooseRandomGame());
            }
            else // si meme jour
            {
                int idxGameOfDay = PlayerPrefs.GetInt("idxGameOfDay", -1);
                Image grayfade = grayFades[idxGameOfDay - 1];
                grayfade.color = new Color(grayfade.color.r, grayfade.color.g, grayfade.color.b, 0f);
                Transform playBtn = playButtons[idxGameOfDay - 1];
                playBtn.GetChild(0).gameObject.SetActive(true); //txt
                playBtn.GetChild(1).gameObject.SetActive(false); //brocoin img
                AudioManagerForOneGame.am.PlayMainTheme();
            }
        }
        else
        {
            PlayerPrefs.SetInt("fullAccess", 1);
            AudioManagerForOneGame.am.PlayMainTheme();
        }
    }

    private IEnumerator ChooseRandomGame()
    {
        yield return null;

        string soundName;
        int modulo = grayFades.Length;
        Color grayColor = grayFades[0].color;
        int curIdx = 0;
        float stopTime = UnityEngine.Random.Range(minTimeCounter, maxTimeCounter);
        float counter = stopTime;
        int previousIdx = 0;

        while (counter > 0)
        {
            float waitTime = Mathf.Clamp(stopTime / (counter * 15f), 0f, 1f);
            counter -= waitTime;
            ChangeColors(previousIdx, curIdx, grayColor);
            soundName = "RandomPick" + curIdx;
            AudioManagerForOneGame.am.PlaySound(soundName);
            previousIdx = curIdx;
            curIdx = (curIdx + 1) % modulo;
            yield return new WaitForSeconds(waitTime);
        }

        /*
        while (counter < stopTime - 10) // de r a 10
        {
            counter += 0.2f;
            soundName = "RandomPick" + curIdx;
            AudioManagerForOneGame.am.PlaySound(soundName);
            ChangeColors(previousIdx, curIdx, grayColor);
            previousIdx = curIdx;
            curIdx = (curIdx + 1) % modulo;
            yield return new WaitForSeconds(0.2f);
        }
        while (counter < stopTime - 6) // de 10 a 6
        {
            counter += 0.3f;
            soundName = "RandomPick" + curIdx;
            AudioManagerForOneGame.am.PlaySound(soundName);
            ChangeColors(previousIdx, curIdx, grayColor);
            previousIdx = curIdx;
            curIdx = (curIdx + 1) % modulo;
            yield return new WaitForSeconds(0.3f);
        }
        while (counter < stopTime - 3) // de 6 a 3
        {
            counter += 0.45f;
            soundName = "RandomPick" + curIdx;
            AudioManagerForOneGame.am.PlaySound(soundName);
            ChangeColors(previousIdx, curIdx, grayColor);
            previousIdx = curIdx;
            curIdx = (curIdx + 1) % modulo;
            yield return new WaitForSeconds(0.45f);
        }
        while (counter < stopTime) // de 3 a 0
        {
            counter += 0.7f;
            soundName = "RandomPick" + curIdx;
            AudioManagerForOneGame.am.PlaySound(soundName);
            ChangeColors(previousIdx, curIdx, grayColor);
            previousIdx = curIdx;
            curIdx = (curIdx + 1) % modulo;
            yield return new WaitForSeconds(0.7f);
        }
        */
        // game is chosen
        int idx = previousIdx;
        soundName = "RandomPick" + idx;
        counter = 0f;
        Image gameGrayFade = grayFades[idx];
        while (counter < 1f)
        {
            gameGrayFade.color = new Color(grayColor.r, grayColor.g, grayColor.b, 0.75f);
            yield return new WaitForSeconds(0.2f);
            counter += 0.2f;
            AudioManagerForOneGame.am.PlaySound(soundName);
            gameGrayFade.color = new Color(grayColor.r, grayColor.g, grayColor.b, 0f);
            yield return new WaitForSeconds(0.2f);
            counter += 0.2f;
        }
        playButtons[idx].GetChild(0).gameObject.SetActive(true);
        playButtons[idx].GetChild(1).gameObject.SetActive(false);
        blocker.SetActive(false);
        PlayerPrefs.SetInt("idxGameOfDay", idx + 1);
        AudioManagerForOneGame.am.PlayMainTheme();
    }


    private void ChangeColors(int previousIdx, int curIdx, Color grayColor)
    {
        grayFades[previousIdx].color = new Color(grayColor.r, grayColor.g, grayColor.b, 0.75f);
        grayFades[curIdx].color = new Color(grayColor.r, grayColor.g, grayColor.b, 0f);
    }


    public void LoadLevel(int buildIdx)
    {
        if (buildIdx == PlayerPrefs.GetInt("idxGameOfDay", -1) || PlayerPrefs.GetInt("fullAccess", 0) == 1)
        {
            AudioManagerForOneGame.am.PlaySound("PlayRestart");
            LoadSceneUtility.LoadLevelAsyncWithFade(blackFadeQuitEnter, buildIdx);
        }
        else if (PlayerPrefs.GetInt("brocoins", 0) > 0)
        {
            AudioManagerForOneGame.am.PlaySound("PlayRestart");
            PlayerPrefs.SetInt("brocoins", PlayerPrefs.GetInt("brocoins", 0) - 1);
            PlayerPrefs.Save();
            LoadSceneUtility.LoadLevelAsyncWithFade(blackFadeQuitEnter, buildIdx);
        }
        else
        {
            AudioManagerForOneGame.am.PlaySound("NoBrocoin");
            warningBrocoin.ShowWarning();
        }
    }

}
