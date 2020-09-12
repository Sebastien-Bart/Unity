using UnityEngine;
using UnityEngine.UI;

public class InGameMenuNew : AbstractMenu
{
    public Sprite soundImgOn, soundImgOff;

    public Image soundImg;

    protected override void Start()
    {
        base.Start();
        if (PlayerPrefs.GetInt("sound", 1) == 1)
            soundImg.sprite = soundImgOn;
        else
            soundImg.sprite = soundImgOff;

        notActivePos = new Vector2(rect.rect.width, 0f);
        rect.anchoredPosition = notActivePos;
    }

    public void changeSound()
    {
        if (AudioListener.volume == 0)
        {
            soundImg.sprite = soundImgOn;
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("sound", 1);
        }
        else if (AudioListener.volume == 1)
        {
            soundImg.sprite = soundImgOff;
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("sound", 0);
        }
    }

}
