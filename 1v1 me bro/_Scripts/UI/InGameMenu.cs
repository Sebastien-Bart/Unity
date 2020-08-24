using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : AbstractMenu
{
    public Image soundButtonImg;
    public Sprite soundOn, soundOff;

    protected override void Start()
    {
        base.Start();
        if (AudioListener.volume == 0)
        {
            if (soundOff)
            {
                soundButtonImg.sprite = soundOff;
            }
        }
        else
        {
            if (soundOn)
            {
                soundButtonImg.sprite = soundOn;
            }
        }
    }

    public void ToMainMenu()
    {
        if (!moving)
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void changeSound()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            if (soundOn)
            {
                soundButtonImg.sprite = soundOn;
            }
        }
        else if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            if (soundOff)
            {
                soundButtonImg.sprite = soundOff;
            }
        }
    }

}
