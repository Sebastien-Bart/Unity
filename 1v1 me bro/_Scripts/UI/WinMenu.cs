using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinMenu : AbstractMenu
{
    [Header("WinText (PAS SALOON)")]
    public TextMeshProUGUI winTxt;

    [Header("SALOON")]
    public GameObject leftWinAnim;
    public GameObject rightWinAnim;
    public Sprite rightPlayerScreen;
    public Sprite leftPlayerScreen;

    public void SetWinner(string winner) // si on est dans Saloon... (nul)
    {
        if (rightWinAnim != null)
        {
            if (winner == "right")
            {
                GetComponent<Image>().sprite = rightPlayerScreen;
                rightWinAnim.SetActive(true);
            }
            else
            {
                GetComponent<Image>().sprite = leftPlayerScreen;
                leftWinAnim.SetActive(true);
            }
        }
        else // si on est dans les autres jeux
        {
            if (winner == "right")
                winTxt.text = "RIGHT PLAYER WINS !";
            else
                winTxt.text = "LEFT PLAYER WINS !";
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
