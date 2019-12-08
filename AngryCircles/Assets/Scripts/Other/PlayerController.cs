using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int nbThrows = 0;
    public static bool thrown = false;
    public static string color = "red";

    public float maxStretch;
    public GameObject allyPrefab;
    public GameObject catapult;
    public Transform rightPlayerLimit, leftPlayerLimit, bottomPlayerLimit;

    private bool powerCalled = false;
    private float resetSpeed = 0.05f;
    private float resetSpeedSqr;
    private bool dragging = false;
    private Vector2 previousVelocity;
    private Vector2 launchPoint;
    private Vector3 initialPosition;

    private SpringJoint2D springJoint;
    private Rigidbody2D rb;

    private void Awake()
    {
        springJoint = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nbThrows = 0;
        launchPoint = new Vector2(catapult.transform.position.x + springJoint.connectedAnchor.x, catapult.transform.position.y + springJoint.connectedAnchor.y);
        initialPosition = transform.position;
        resetSpeedSqr = resetSpeed * resetSpeed;
    }

    private void Update()
    {

        dragTheCircle();

        if (!thrown)
        {
            if (!rb.isKinematic && previousVelocity.sqrMagnitude > rb.velocity.sqrMagnitude)
            {
                springJoint.enabled = false;
                thrown = true;
                rb.velocity = previousVelocity;
                nbThrows++;
            }
            else
            {
                previousVelocity = rb.velocity;
            }
        }

        if (Input.GetMouseButtonDown(0) && thrown)
        {
            power();
        }

        resetPlayer();
    }

    private void OnMouseDown()
    {
        if (!thrown && !InGameMenuManagement.isPaused)
        {
            rb.isKinematic = true;
            dragging = true;
            springJoint.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        if (!thrown && !InGameMenuManagement.isPaused)
        {
            dragging = false;
            rb.isKinematic = false;
            springJoint.enabled = true;
        }
    }

    private void dragTheCircle()
    {
        if (dragging && !thrown)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            Vector2 fromLaunchPointToMouse = mousePosition2D - launchPoint;

            if(fromLaunchPointToMouse.magnitude < maxStretch) // le  joueur reste dans la zone d'étirement possible
            {
                transform.position = mousePosition2D;
            }

            else // Le joueur depasse la zone d'etirement maximale
            {
                Vector2 newPosition = Vector2.ClampMagnitude(fromLaunchPointToMouse, maxStretch) + launchPoint;
                transform.position = newPosition;
            }
        }
    }

    private void power()
    {
        if (!powerCalled)
        {
            if (color == "red")
            {
                // rend n'a pas de pouvoir
            }
            else if (color == "yellow")
            {
                rb.AddForce(new Vector2(25, 0), ForceMode2D.Impulse);
            }
            else if (color == "blue")
            {
                float offset = 0.5f;

                Vector2 allyOnePosition = transform.position;
                allyOnePosition.y += offset;
                Vector2 allyTwoPosition = transform.position;
                allyTwoPosition.y -= offset;

                Vector2 force = rb.velocity;
                Vector2 allyOneForce = force + new Vector2(0, 10);
                Vector2 allyTwoForce = force + new Vector2(0, -10);

                GameObject ally1 = Instantiate(allyPrefab, allyOnePosition, Quaternion.identity);
                GameObject ally2 = Instantiate(allyPrefab, allyTwoPosition, Quaternion.identity);

                ally1.GetComponent<Rigidbody2D>().AddForce(allyOneForce, ForceMode2D.Impulse);
                ally2.GetComponent<Rigidbody2D>().AddForce(allyTwoForce, ForceMode2D.Impulse);
            }
        }
        powerCalled = true;
    }

    private void resetPlayer()
    {
        if (thrown && (transform.position.x > rightPlayerLimit.position.x || transform.position.x < leftPlayerLimit.position.x || transform.position.y < bottomPlayerLimit.position.y ||rb.velocity.sqrMagnitude < resetSpeedSqr))
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            transform.position = initialPosition;
            thrown = false;
            powerCalled = false;
            GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
            if (allies.Length > 0)
            {
                foreach (GameObject ally in allies)
                {
                    Destroy(ally);
                }
            }
        }
    }

}
