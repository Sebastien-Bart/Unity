public class CollectibleStarPowerUp : PowerUp
{
    public override void ActivatePowerUp(StarshipController pickerStarship)
    {
        pickerStarship.pointManager.AddPoints(1);
    }
}
