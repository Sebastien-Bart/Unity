using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPlayerCollision : MonoBehaviour
{

    private Transform player;

    private void Start()
    {
        player = transform.parent;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.bounds.min.y > collision.otherCollider.bounds.max.y)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (!(rb.velocity.y < 0))
            {
                player.GetComponent<PlayerController>().setJumpTimeCounterToZero();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.bounds.min.y > collision.otherCollider.bounds.max.y)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (!(rb.velocity.y < 0))
            {
                player.GetComponent<PlayerController>().setJumpTimeCounterToZero();
            }
        }
    }
}
