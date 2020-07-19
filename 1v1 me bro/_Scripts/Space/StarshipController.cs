using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipController : MonoBehaviour
{
    [Header("other Starship")]
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

    private Vector2 spawnPos; 
    public Vector2 SpawnPos { get => spawnPos; }

    private Quaternion spawnRot;
    public Quaternion SpawnRot { get => spawnRot;}

    // Off camera related attributes
    private float minXcam;
    private float maxXcam;
    private float minYcam;
    private float maxYcam;
    private Camera cam;

    
    private Rigidbody2D rb;


    void Start()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        baseShipSpeed = shipSpeed;

        // Off camera related 
        cam = Camera.main;
        minXcam = cam.ScreenToWorldPoint(Vector3.zero).x;
        maxXcam = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f, 0f)).x;
        minYcam = cam.ScreenToWorldPoint(Vector3.zero).y;
        maxYcam = cam.ScreenToWorldPoint(new Vector3(0f, cam.pixelHeight, 0f)).y;

        canGoForward = true;
        rotating = true;
        rb = GetComponent<Rigidbody2D>();
        outlineSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Rotate();
        CheckIfCanGoForward();
        TPWhenOffCamera();
    }

    // Pour deathTouch
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if(gameobject.tag == vaisseau) pas obligé, il n'y a queuux deux qui ont des colliders non triggers
        if (deathTouchOn)
        {
            outlineSR.color = Color.black;
            deathTouchOn = false;
            deathManager.Kill(collision.gameObject);
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
        if (canGoForward)
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

    private void TPWhenOffCamera()
    {
        if (transform.position.x < minXcam)
            transform.position = new Vector3(maxXcam, transform.position.y, transform.position.z);
        else if (transform.position.x > maxXcam)
            transform.position = new Vector3(minXcam, transform.position.y, transform.position.z);
        else if (transform.position.y < minYcam)
            transform.position = new Vector3(transform.position.x, maxYcam, transform.position.z);
        else if (transform.position.y > maxYcam)
            transform.position = new Vector3(transform.position.x, minYcam, transform.position.z);
    }

    

    

}
