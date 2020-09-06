using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipController : MonoBehaviour
{
    [Header("StarshipPointManager")]
    public StarshipPointManager pointManager;

    [Header("TpOffCamera")]
    public TpOffCamera tpOffCamera;

    [Header("Death Manager")]
    public DeathManager deathManager;

    [Header("other Starship")]
    public GameObject otherStarship;

    [Header("Starship settings")]
    public float rotateSpeed = 150f;
    public float shipSpeed = 10f;
    public float minMagSqrAbleGoForward = 15;
    public float minMagSqrAbleRotate = 10;
    

    [NonSerialized] public SpriteRenderer outlineSR;
    [NonSerialized] public bool deathTouchOn = false;

    private float baseShipSpeed;

    private bool rotating;
    private bool canGoForward;

    public Vector2 spawnPos { get; private set; } 

    public Quaternion spawnRot { get; private set; }

    private Rigidbody2D rb;


    void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        baseShipSpeed = shipSpeed;

        canGoForward = true;
        rotating = true;
        rb = GetComponent<Rigidbody2D>();
        outlineSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Rotate();
        CheckIfCanGoForward();
        tpOffCamera.TPWhenOffCamera(transform);
    }

    // Pour deathTouch
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "starship")
        {
            if (deathTouchOn)
            {
                outlineSR.color = Color.black;
                deathTouchOn = false;
                deathManager.Kill(collision.gameObject);
            }
            if (shipSpeed != baseShipSpeed)
            {
                outlineSR.color = Color.green;
            }
        }
    }

    public void CheckIfCanGoForward()
    {
        if (rb.velocity.sqrMagnitude < minMagSqrAbleGoForward)
        {
            canGoForward = true;
        }
    }

    public void GoForward()
    {
        if (canGoForward && !InGameMenuNew.Paused)
        {
            rotating = false;
            canGoForward = false;
            rb.AddRelativeForce(new Vector2(shipSpeed, 0f), ForceMode2D.Impulse);

            if (shipSpeed != baseShipSpeed) // pour power up boost
            {
                if (!deathTouchOn)
                    outlineSR.color = Color.black;
                else
                    outlineSR.color = Color.red;
            }
            shipSpeed = baseShipSpeed; 
        }
    }

    private void Rotate()
    {
        if (rb.velocity.sqrMagnitude < minMagSqrAbleRotate)
        {
            rotating = true;
        }
        if (rotating)
        {
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
    }

}
