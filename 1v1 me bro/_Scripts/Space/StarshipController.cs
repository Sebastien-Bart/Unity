using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipController : MonoBehaviour
{
    [Header("other Starship")]
    public GameObject otherStarship;

    [Header("Starship settings")]
    public float rotateSpeed = 150f;
    public float shipSpeed = 10f;
    public float minMagSqrAbleGoForward = 15;
    public float minMagSqrAbleRotate = 10;

    [NonSerialized]
    public SpriteRenderer outlineSR;

    private float baseShipSpeed;

    private bool rotating;
    private bool canGoForward;

    // Off camera related attributes
    private float minXcam;
    private float maxXcam;
    private float minYcam;
    private float maxYcam;
    private Camera cam;

    
    private Rigidbody2D rb;

    void Start()
    {
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
                outlineSR.color = Color.black;

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
