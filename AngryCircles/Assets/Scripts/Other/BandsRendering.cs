using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandsRendering : MonoBehaviour
{
    public GameObject circle;

    private LineRenderer lineRenderer;
    private CircleCollider2D circleCollider;
    private Ray rayFromThisToCircle;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        circleCollider = circle.GetComponent<CircleCollider2D>();
        rayFromThisToCircle = new Ray(transform.position, Vector3.zero);
    }

    private void Start()
    {
        lineRenderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        if (!PlayerController.thrown)
        {
            lineRenderer.enabled = true;
            Vector3 vectorFromThisToCircle = circle.transform.position - transform.position;
            rayFromThisToCircle.direction = vectorFromThisToCircle;
            float lengthOfLine = vectorFromThisToCircle.magnitude + circleCollider.radius;
            lineRenderer.SetPosition(1, rayFromThisToCircle.GetPoint(lengthOfLine));

            /** float actualMagnitude = position.magnitude;                         Ne comprends pas pq ca ne fonctionne pas comme voulu
            float wantedMagnitude = position.magnitude + circleCollider.radius; 
            float coef = wantedMagnitude / actualMagnitude;
            lineRenderer.SetPosition(1, position * coef); */
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
