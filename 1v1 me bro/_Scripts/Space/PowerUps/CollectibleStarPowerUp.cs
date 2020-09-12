public class CollectibleStarPowerUp : PowerUp
{

    protected void Start()
    {
        nameSoundEffect = "PowerUpStar";
    }

    public override void ActivatePowerUp(StarshipController pickerStarship)
    {
        pickerStarship.pointManager.AddPoints(1);
    }
}
