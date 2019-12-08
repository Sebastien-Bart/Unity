using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManagement : MonoBehaviour
{

    public GameObject generalMenu;
    public GameObject powerMenu;
    public GameObject helpMenu;

    public static bool isPaused;

    private void Start()
    {
        isPaused = false;
        generalMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    private void Update() // mettre en pause avec certaines touches claviers
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Return))
            pauseUnpauseGame();
        if (Input.GetKeyDown(KeyCode.R))
            reloadCurrentScene();

        if (PlayerController.thrown)
        {
            powerMenu.SetActive(false);
        }
        else
        {
            powerMenu.SetActive(true);
        }
    }



    public void pauseUnpauseGame() // pour bouttons "Menu" (en haut a droite) et "Continue"
    {
        if (isPaused)
        {
            Time.timeScale = 1; // on relance le jeu et cache le menu
            generalMenu.SetActive(false);
            helpMenu.SetActive(false);
            powerMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 0; // on pause et montre le menu
            generalMenu.SetActive(true);
            powerMenu.SetActive(false);
        }

        isPaused = !isPaused;
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void reloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void showHelp()
    {
        helpMenu.SetActive(true);
    }

    public void quitHelp()
    {
        helpMenu.SetActive(false);
    }

}
