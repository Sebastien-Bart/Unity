using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTouch : PowerUp
{
    public override void ActivatePowerUp()
    {
        pickerStarship.outlineSR.color = Color.red;
        pickerStarship.deathTouchOn = true;
    }

}
