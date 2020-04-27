using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : AbstractMenu
{

    protected override void Start()
    {
        base.Start();
        base.ActivateMenu();
    }

    public void CloseHelpMenuAndStartGame()
    {
        base.DeactivateMenu();
        Time.timeScale = 1;
    }

}
