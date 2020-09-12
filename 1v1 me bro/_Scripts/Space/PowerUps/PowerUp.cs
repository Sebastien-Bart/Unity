using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public ParticleSystem pickedParticle;

    protected string nameSoundEffect = "PickPowerUp";

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "starship")
        {
            AudioManagerForOneGame.am.PlaySound(nameSoundEffect);
            ParticleSystem particle = Instantiate(pickedParticle);
            particle.transform.position = transform.position;
            particle.Play();
            StarshipController pickerStarship = collision.gameObject.GetComponent<StarshipController>();
            ActivatePowerUp(pickerStarship);
            Destroy(gameObject);
        }
    }

    public abstract void ActivatePowerUp(StarshipController pickerStarship);

}
