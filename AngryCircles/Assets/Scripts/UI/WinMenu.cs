using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{

    public GameObject gameMenuButton;

    private bool called = false;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Ennemy").Length == 0 && !called)
            showWinMenu();
    }

    private void showWinMenu()
    {
        gameMenuButton.SetActive(false);
        Time.timeScale = 0;
        transform.GetChild(0).gameObject.SetActive(true);
        called = true;
    }

}
