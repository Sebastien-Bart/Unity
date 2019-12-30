using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GroundedDetection : MonoBehaviour
{

    private Transform character;

    private void Start()
    {
        character = transform.parent;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject != character)
        {
            character.GetComponent<PlayerController>().isGrounded = true;
            character.GetComponent<PlayerController>().playLanding();
            character.GetComponent<PlayerController>().resetExtraJumpsAvailable();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        character.GetComponent<PlayerController>().isGrounded = false;
    }
}
