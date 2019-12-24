using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform goal;

    private Rigidbody2D rb;
    private Animation jumpAnimation;

    private bool jumping = false;
    private bool jumpedPressed;
    private float horizontalMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpAnimation = GetComponent<Animation>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        jumpedPressed = Input.GetButton("Jump");

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        if (jumpedPressed)
            Jump();
    }

    private void Jump()
    {
        if(!jumping)
        {
            rb.AddForce(new Vector2(0, 1 * jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpAnimation.Play("jump");
        }
    }

    public void setJumpingFalse()
    {
        jumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            jumpAnimation.Play("GoalAnimation");
        }
    }

}
