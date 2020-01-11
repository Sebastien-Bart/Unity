using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GroundedDetection : MonoBehaviour
{

    private Transform player;

    private void Start()
    {
        player = transform.parent;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.bounds.max.y < transform.position.y)
        {
            if (collision.gameObject != player)
            {
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if(!(rb.velocity.y > 0))
                {
                    player.GetComponent<PlayerController>().isGrounded = true;
                    player.GetComponent<PlayerController>().playLanding();
                    player.GetComponent<PlayerController>().resetExtraJumpsAvailable();
                    player.GetComponent<PlayerController>().resetJumpTimeCounter();
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.bounds.max.y < transform.position.y)
        {
            if (collision.gameObject != player)
            {
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if (!(rb.velocity.y >0))
                {
                    player.GetComponent<PlayerController>().isGrounded = true;
                    player.GetComponent<PlayerController>().resetJumpTimeCounter();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.GetComponent<PlayerController>().isGrounded = false;
    }
}
