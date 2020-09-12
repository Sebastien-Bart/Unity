using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinMenu : AbstractMenu
{
    public WarningBrocoin warningBrocoin;

    public GameObject brocoin;
    public TextMeshProUGUI winTxt;
    /*
    public GameObject leftWinAnim;
    public GameObject rightWinAnim;
    */

    protected override void Start()
    {
        base.Start();
        rect.anchoredPosition = Vector2.zero;
        notActivePos = new Vector2(rect.anchoredPosition.x + rect.rect.width, rect.anchoredPosition.y);
        rect.anchoredPosition = notActivePos;
        if (PlayerPrefs.GetInt("idxGameOfDay", -1) == SceneManager.GetActiveScene().buildIndex || PlayerPrefs.GetInt("fullAccess", 0) == 1)
            brocoin.gameObject.SetActive(false);
    }

    public void SetWinner(string winner)
    {
        if (winner == "right")
        {
            winTxt.text = "RIGHT PLAYER WINS !";
            //rightWinAnim.SetActive(true);
        }
        else if (winner == "left")
        {
            winTxt.text = "LEFT PLAYER WINS !";
            //leftWinAnim.SetActive(true);
        }
        else
        {
            winTxt.text = "IT'S A TIE !";
        }
    }

    public void Replay()
    {
        int buildIdx = SceneManager.GetActiveScene().buildIndex;
        if (buildIdx == PlayerPrefs.GetInt("idxGameOfDay", -1) || PlayerPrefs.GetInt("fullAccess", 0) == 1)
        {
            AudioManagerMenu.am.PlaySound("PlayRestart");
            LoadSceneUtility.LoadLevelAsyncWithFade(blackFadeQuitEnter, buildIdx);
            Unpause();
        }
        else if (PlayerPrefs.GetInt("brocoins", 0) > 0)
        {
            AudioManagerMenu.am.PlaySound("PlayRestart");
            PlayerPrefs.SetInt("brocoins", PlayerPrefs.GetInt("brocoins", 0) - 1);
            PlayerPrefs.Save();
            LoadSceneUtility.LoadLevelAsyncWithFade(blackFadeQuitEnter, buildIdx);
            Unpause();
        }
        else
        {
            AudioManagerMenu.am.PlaySound("NoBrocoin");
            warningBrocoin.ShowWarning();
        }
    }

}
