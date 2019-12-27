using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int nbJumps = 1;
    [SerializeField] private Transform groundCheckTopLeft;
    [SerializeField] private Transform groundCheckBottomRight;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject goal;


    private Rigidbody2D rb;
    private Animation playerAnimation;

    private bool isGrounded = false;
    private bool movable = true;
    private bool jumping = false;
    private bool jumpPressed = false;
    private float horizontalMovement;
    private int currentNbJumps;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animation>();
        currentNbJumps = nbJumps;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckTopLeft.position, groundCheckBottomRight.position, layerMask);
        horizontalMovement = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");

        if (isGrounded)
        {
            currentNbJumps = nbJumps;
            jumping = false;
        }
    }

    void FixedUpdate()
    {
        if (movable)
        {
            rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
            if (jumpPressed)
            {
                if (isGrounded && currentNbJumps > 0)
                {
                    jumping = true;
                    Jump();
                }
                else if (jumping && currentNbJumps > 0) {
                    Jump();
                }
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, 1 * jumpForce), ForceMode2D.Impulse);
        jumping = true;
        playerAnimation.Play("jump");
        currentNbJumps--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == goal)
        {
            movable = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            StartCoroutine(MoveTo(transform, goal.transform.position, 0.7f));
        }
    }

    public IEnumerator MoveTo(Transform toMove, Vector2 destination, float speed)
    {
        while((Vector2) toMove.position != destination)
        {
            Vector3 newPosition = Vector3.MoveTowards(toMove.position, destination, speed * Time.deltaTime);
            newPosition.z = 0;
            toMove.position = newPosition;
            yield return null;
        }
        Destroy(goal);
        playerAnimation.Play("goal");
    }
}
