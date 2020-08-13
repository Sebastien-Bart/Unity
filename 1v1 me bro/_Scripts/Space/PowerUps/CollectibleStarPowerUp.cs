using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CollectibleStarPowerUp : PowerUp
{
    public float rotateSpeed;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        // A REMPLACER PAR IDLE ANIMATION
        transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime));
    }

    public override void ActivatePowerUp(StarshipController pickerStarship)
    {
        pickerStarship.AddPoint();
    }
}
