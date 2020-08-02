using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    private DeathManager deathManager;

    public float speed;
    public float speedRotation;

    public ParticleSystem explosion;

    private StarshipController targetStarship;

    public void SetAttributes(StarshipController picker)
    {
        targetStarship = picker.otherStarship.GetComponent<StarshipController>();
        deathManager = targetStarship.deathManager;
    }

    void Update()
    {
        Vector3 direction = targetStarship.transform.position - transform.position;
        transform.right = Vector2.MoveTowards(transform.right, direction, speedRotation * Time.deltaTime);
        Vector3 mouvement = Vector2.MoveTowards(transform.position, targetStarship.transform.position, speed * Time.deltaTime);
        transform.position = mouvement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem p = Instantiate(explosion, transform.position, Quaternion.identity);
        p.Play();
        // SI CEST UN AUTRE MISSILE ? ;D
        if (collision.gameObject.tag == "starship")
        {
            deathManager.Kill(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
