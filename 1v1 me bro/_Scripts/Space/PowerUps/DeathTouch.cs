using UnityEngine;

public class DeathTouch : PowerUp
{
    public override void ActivatePowerUp(StarshipController pickerStarship)
    {
        pickerStarship.outlineSR.color = Color.red;
        pickerStarship.deathTouchOn = true;
    }

}
