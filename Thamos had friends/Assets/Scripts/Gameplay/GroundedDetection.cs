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
                player.GetComponent<PlayerController>().isGrounded = true;
                player.GetComponent<PlayerController>().playLanding();
                player.GetComponent<PlayerController>().resetExtraJumpsAvailable();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) // a enlever si il faut opti
    {
        if (collision.collider.bounds.max.y < transform.position.y)
        {
            if (collision.gameObject != player)
            {
                player.GetComponent<PlayerController>().isGrounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.GetComponent<PlayerController>().isGrounded = false;
    }
}
