using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    [Range(0f, 120f)] public float magnitude = 50f;
    public Transform shooter;
    
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tag = "bullet";
    }

    public void SetPositionAndDirection(Transform player, bool goodShot, ParticleSystem shoot_particle)
    {
        shoot_particle.Play();
        shooter = player;
        Transform gunPosition = shooter.GetChild(0);
        transform.position = new Vector3(gunPosition.position.x, gunPosition.position.y, gunPosition.position.z);
        if (goodShot)
        {
            MakeGoodShot();
        }
        else
        {
            MakeBadShot();
        }
    }

    private void MakeBadShot()
    {
        float x = Random.Range(0f, magnitude);
        float y = Mathf.Sqrt(magnitude * magnitude - x * x);
        float rotationZ = y * 0.85f;
        if (shooter.position.x > 0)
        {
            rb.velocity = new Vector2(-x, y);
            rb.transform.Rotate(0f, 0f, -rotationZ);
        }
        else
        {
            rb.velocity = new Vector2(x, y);
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y);
            rb.transform.Rotate(0f, 0f, rotationZ);
        }
    }

    private void MakeGoodShot()
    {
        if (shooter.position.x > 0)
            rb.velocity = new Vector2(-magnitude, 0);
        else
        {
            rb.velocity = new Vector2(magnitude, 0);
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y); // inverser sprite bullet
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
