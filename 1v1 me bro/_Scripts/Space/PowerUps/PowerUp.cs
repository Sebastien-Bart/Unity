using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public ParticleSystem pickedParticle;

    protected StarshipController pickerStarship;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem particle = Instantiate(pickedParticle);
        particle.transform.position = transform.position;
        particle.Play();
        pickerStarship = collision.gameObject.GetComponent<StarshipController>();
        ActivatePowerUp();
        Destroy(gameObject);
    }

    public abstract void ActivatePowerUp();

}
