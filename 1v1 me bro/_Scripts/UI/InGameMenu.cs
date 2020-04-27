using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : AbstractMenu
{
    public Image soundButtonImg;
    public Sprite soundOn, soundOff;

    public void ToMainMenu()
    {
        if (!moving)
            SceneManager.LoadScene(0);
    }

    public void changeSound()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            soundButtonImg.sprite = soundOn;
        }
        else if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            soundButtonImg.sprite = soundOff;
        }
    }

}
