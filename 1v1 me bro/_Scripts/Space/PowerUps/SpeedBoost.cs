using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PowerUp
{
    [SerializeField]
    private float newSpeed = 30f;

    public override void ActivatePowerUp()
    {
        pickerStarship.outlineSR.color = Color.green;
        pickerStarship.shipSpeed = newSpeed;
    }

}
