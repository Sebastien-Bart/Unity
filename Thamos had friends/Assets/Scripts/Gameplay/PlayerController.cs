using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe abstraite pour gerer un character
/// </summary>
public abstract class PlayerController : MonoBehaviour {

    protected string characterName; // pour jouer les animations propres a chaque character

    protected abstract string giveCharacterName();

    public bool jumping = false;
    public bool movable = false;
    public bool isFocus;
    [Range(0, 5)] public int extraJumps = 0;

    [SerializeField] [Range(1, 20)] protected float speed = 10f;
    [SerializeField] [Range(1, 20)] protected float jumpForce = 10f;
    [SerializeField] [Range(0.1f, 0.3f)] protected float jumpTime = 0.2f;
    [SerializeField] [Range(0.1f, 1f)] protected float moveToGoalSpeed = 0.7f;
    [SerializeField] protected GameObject goal;
    [SerializeField] protected GameObject focusIndicator;

    protected Rigidbody2D rb;
    protected Animation playerAnimation;
    protected SpriteRenderer spriteRenderer;
    protected Color initialColor;
    
    protected float horizontalMovement;

    protected bool canBeginJump = true;
    protected bool onGoal = false;
    public bool isGrounded = false;

    protected int extraJumpsAvailable;
    [SerializeField] protected float jumpTimeCounter;

    // --------------------------------- FONCTIONS API UNITY -------------------------------------

    // ----- START / UPDATE(s) -----

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animation>();
        initialColor = spriteRenderer.color;
        characterName = giveCharacterName();
        extraJumpsAvailable = extraJumps;
        jumpTimeCounter = jumpTime;
    }

    protected void Update()
    {
        if (movable)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            jumpManagement();
        } else
        {
            horizontalMovement = 0;
        }
    }

    protected void FixedUpdate()
    {
       rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
    }

    protected void jumpManagement()
    {
        if (Input.GetButton("Jump"))
        {
            if (canBeginJump)
            {
                if (isGrounded)
                {
                    playerAnimation.Play(characterName + "Jump");
                    canBeginJump = false;
                    jumping = true;
                }
                else if (extraJumpsAvailable > 0)
                {
                    canBeginJump = false;
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
            else
            {
                jumping = false;
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            canBeginJump = true;
            jumpTimeCounter = jumpTime;
            jumping = false;
        }
    }

    // ----- TRIGGER -----

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == goal)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            onGoal = true;
            if (checkIfAllOnGoal())
            {
                movable = false;
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                focusIndicator.SetActive(false);
                StartCoroutine(moveToGoal(transform, goal.transform.position, moveToGoalSpeed));
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == goal && !checkIfAllOnGoal())
        {
            spriteRenderer.color = initialColor;
            onGoal = false;
        }
    }

    // ---------------------------------------- MES FONCTIONS ----------------------------------

    public void setJumpTimeCounterToZero()
    {
        jumpTimeCounter = 0;
    }

    public void resetJumpTimeCounter()
    {
        jumpTimeCounter = jumpTime;
    }

    public void playLanding()
    {
        if (movable)
        {
            playerAnimation.Play(characterName + "Landing");
        }
    }

    public void setFocus(bool focus)
    {
        isFocus = focus;
        focusIndicator.SetActive(focus);
        movable = focus;
    }

    public void resetExtraJumpsAvailable()
    {
        extraJumpsAvailable = extraJumps;
    }

    public void addEXtraJump()
    {
        extraJumps++;
    }

    protected bool checkIfAllOnGoal()
    {
        foreach (Transform character in transform.parent)
        {
            PlayerController characterScript = character.GetComponent<PlayerController>();
            if (!characterScript.isOnGoal())
            {
                return false;
            }
        }
        return true;
    }

    protected bool isOnGoal()
    {
        return onGoal;
    }

    protected IEnumerator moveToGoal(Transform toMove, Vector2 destination, float speed)
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
