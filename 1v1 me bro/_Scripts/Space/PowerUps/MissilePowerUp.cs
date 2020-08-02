using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePowerUp : PowerUp
{
    public GameObject missilePrefab;
    public float distance = 4f;

    public override void ActivatePowerUp(StarshipController pickerStarship)
    {
        GameObject missile = Instantiate(missilePrefab);
        missile.transform.position = pickerStarship.transform.position + distance * pickerStarship.transform.right * -1f;
        missile.GetComponent<MissileBehavior>().SetAttributes(pickerStarship);
    }

}
