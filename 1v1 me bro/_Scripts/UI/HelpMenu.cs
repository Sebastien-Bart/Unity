using UnityEngine;

public class HelpMenu : AbstractMenu
{

    protected override void Start()
    {
        LoadSceneUtility.FadeOnLevelLoaded(blackFadeQuitEnter);
        base.Start();
        if (PlayerPrefs.GetInt("helpMenus", 1) == 1)
        {
            Time.timeScale = 0;
            base.ActivateMenu();
        }
            
    }

}
