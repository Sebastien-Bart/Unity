using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController.setJumpingFalse();
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

}
