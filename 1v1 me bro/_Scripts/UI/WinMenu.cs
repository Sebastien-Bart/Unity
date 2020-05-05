using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : AbstractMenu
{
    public GameObject leftWinAnim;
    public GameObject rightWinAnim;
    public Sprite rightPlayerScreen;
    public Sprite leftPlayerScreen;

    public void SetWinner(string winner)
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

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
