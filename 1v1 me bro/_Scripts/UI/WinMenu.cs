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
        if (PlayerPrefs.GetInt("idxGameOfDay", -1) == SceneManager.GetActiveScene().buildIndex || PlayerData.hasFullAccess)
            brocoin.gameObject.SetActive(false);
    }

    public void SetWinner(string winner)
    {
        string lang = PlayerPrefs.GetString("lang", "en");
        if (winner == "right")
        {
            if (lang == "fr")
                winTxt.text = "le joueur a droite gagne!";
            else
                winTxt.text = "RIGHT PLAYER WINS!";
            //rightWinAnim.SetActive(true);
        }
        else if (winner == "left")
        {
            if (lang == "fr")
                winTxt.text = "le joueur a gauche gagne!";
            else
                winTxt.text = "LEFT PLAYER WINS!";
            //leftWinAnim.SetActive(true);
        }
        else
        {
            if (lang == "fr")
                winTxt.text = "egalite!";
            else
                winTxt.text = "IT'S A TIE!";
        }
    }

    public void Replay()
    {
        int buildIdx = SceneManager.GetActiveScene().buildIndex;
        if (buildIdx == PlayerPrefs.GetInt("idxGameOfDay", -1) || PlayerData.hasFullAccess)
        {
            AudioManagerMenu.am.PlaySound("PlayRestart");
            LoadSceneUtility.LoadLevelAsyncWithFade(blackFadeQuitEnter, buildIdx);
            Unpause();
        }
        else if (PlayerData.nbBrocoins > 0)
        {
            AudioManagerMenu.am.PlaySound("PlayRestart");
            PlayerData.nbBrocoins -= 1;
            PlayerData.SaveBrocoinsAndAccess();
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
