using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLifeSystem : MonoBehaviour
{

    public Transform leftLimit, rightLimit, bottomLimit;

    public int lifePoints = 3;
    public float damagingSpeed = 3;

    private float damagingSpeedSqr;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        damagingSpeedSqr = damagingSpeed * damagingSpeed;
    }

    private void Update()
    {
        if (transform.position.x < leftLimit.position.x || transform.position.x > rightLimit.position.x || transform.position.y < bottomLimit.position.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.rigidbody.velocity.sqrMagnitude > damagingSpeedSqr || rb.velocity.sqrMagnitude > damagingSpeedSqr)
        {
            lifePoints--;
        }
        else if (collision.gameObject.CompareTag("Player") && collision.rigidbody.velocity.sqrMagnitude > damagingSpeedSqr)
        {
            lifePoints -= 2;
        }

        if (lifePoints < 1)
            Destroy(gameObject);

    }

}
