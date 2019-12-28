using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour {

    protected string characterName;

    protected abstract string giveCharacterName();
    
    [SerializeField] [Range(1, 20)] protected float speed = 10f;
    [SerializeField] [Range(1, 20)] protected float jumpForce = 10f;
    [SerializeField] [Range(0, 5)] protected int extraJumps = 0;
    [SerializeField] [Range(0.1f, 0.3f)] protected float jumpTime = 1.0f;
    [SerializeField] protected Transform groundCheckTopLeft;
    [SerializeField] protected Transform groundCheckBottomRight;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected GameObject goal;
    [SerializeField] protected GameObject focusIndicator;


    protected Rigidbody2D rb;
    protected Animation playerAnimation;

    
    protected float horizontalMovement;

    protected bool onGoal = false;
    protected bool landingPlayed = false;
    protected bool jumping = false;
    protected bool isOnFocus = true; // A CHANGER
    protected bool isGrounded = false;
    protected bool movable = true;
    protected int extraJumpsAvailable;
    protected float jumpTimeCounter;

    protected void Start()
    {
        characterName = giveCharacterName();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animation>();
        extraJumpsAvailable = extraJumps;
        jumpTimeCounter = jumpTime;
    }

    protected void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckTopLeft.position, groundCheckBottomRight.position, layerMask);

        horizontalMovement = Input.GetAxis("Horizontal");

        jumpManagement();

        if (isGrounded)
        {
            playLanding();
            extraJumpsAvailable = extraJumps;
        }
        else
        {
            landingPlayed = false;
        }

        if (!isOnFocus)
        {
            focusIndicator.SetActive(false);
        }
    }

    protected void FixedUpdate()
    {
        if (movable)
        {
            rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        }
    }

    protected void jumpManagement()
    {
        if (movable)
        {
            if (Input.GetButton("Jump"))
            {
                if (isGrounded)
                {
                    jumping = true;
                    if (Input.GetButtonDown("Jump"))
                        playerAnimation.Play(characterName + "Jump");
                }
                else if (extraJumpsAvailable > 0)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        jumping = true;
                        playerAnimation.Stop(characterName + "Jump"); // si double saut rapide
                        playerAnimation.Play(characterName + "Jump");
                        extraJumpsAvailable--;
                    }
                }

                if(jumping && jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 1 * jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
            else if (Input.GetButtonUp("Jump"))
            {
                jumpTimeCounter = jumpTime;
                jumping = false;
            }
        }
    }

    protected void playLanding()
    {
        if (!landingPlayed && !onGoal)
        {
            playerAnimation.Play(characterName + "Landing");
            landingPlayed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == goal)
        {
            onGoal = true;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == goal)
        {
            movable = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            StartCoroutine(MoveTo(transform, goal.transform.position, 0.7f));
        }
    }

    protected IEnumerator MoveTo(Transform toMove, Vector2 destination, float speed)
    {
        while((Vector2) toMove.position != destination)
        {
            Vector3 newPosition = Vector3.MoveTowards(toMove.position, destination, speed * Time.deltaTime);
            newPosition.z = 0;
            toMove.position = newPosition;
            yield return null;
        }
        Destroy(goal);
        playerAnimation.Play(characterName + "Goal");
    }
}
