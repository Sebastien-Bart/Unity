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
        player.GetComponent<PlayerController>().jumping = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        player.GetComponent<PlayerController>().jumping = false;
    }
}
