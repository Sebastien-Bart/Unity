using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [Range(3f,10f)] public float lifetime;
    public float speed;
    public float speedRotation;

    public ParticleSystem explosion;

    private Animator animator;

    private TpOffCamera tpOffCamera;
    private StarshipController targetStarship;
    private DeathManager deathManager;

    private void Start()
    {
        tpOffCamera = Camera.main.GetComponent<TpOffCamera>();
        animator = GetComponent<Animator>();
        StartCoroutine("Lifetime");
    }

    public void SetAttributes(StarshipController picker)
    {
        targetStarship = picker.otherStarship.GetComponent<StarshipController>();
        deathManager = targetStarship.deathManager;
    }

    void Update()
    {
        tpOffCamera.TPWhenOffCamera(transform);
        Vector3 direction;
        Vector3 movement;
        if (targetStarship.gameObject.activeSelf)
        {
            direction = targetStarship.transform.position - transform.position;
            movement = Vector2.MoveTowards(transform.position, targetStarship.transform.position, speed * Time.deltaTime); 
            transform.right = Vector2.MoveTowards(transform.right, direction, speedRotation * Time.deltaTime);
        }
        else
        {
            movement = Vector2.MoveTowards(transform.position, transform.position + transform.right, speed * Time.deltaTime);
        }
        transform.position = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem p = Instantiate(explosion, transform.position, Quaternion.identity);
        p.Play();
        // SI CEST UN AUTRE MISSILE ?
        if (collision.gameObject.tag == "starship")
        {
            deathManager.Kill(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator Lifetime()
    {
        float counter = 0f;
        bool playingAnim = false;
        while (counter < lifetime)
        {
            counter += Time.deltaTime;
            if (lifetime - counter < 3f && !playingAnim)
            {
                animator.Play("missileRed");
                playingAnim = true;
            }
            yield return null;
        }
        ParticleSystem p = Instantiate(explosion, transform.position, Quaternion.identity);
        p.Play();
        Destroy(gameObject);
    }

}
