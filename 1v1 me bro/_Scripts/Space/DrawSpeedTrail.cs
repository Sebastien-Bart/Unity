using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpeedTrail : MonoBehaviour
{
    public float minSpeedTrail;
    public int nbPoints;
    public Rigidbody2D rbStarship;


    private LineRenderer lr;
    private List<Vector3> points;
    private bool drawing = false;

    void Start()
    {
        points = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (rbStarship.velocity.sqrMagnitude > minSpeedTrail && !drawing)
            StartCoroutine(StartDrawTrail());
    }

    private IEnumerator StartDrawTrail()
    {
        drawing = true;
        points.Clear();
        lr.positionCount = 0;
        int i = 0;
        while (lr.positionCount < nbPoints)
        {
            lr.positionCount++;
            Vector2 point = transform.position;
            points.Add(point);
            lr.SetPosition(i, point);
            i++;
            yield return null;
        }
        StartCoroutine(ContinueTrail());
    }

    private IEnumerator ContinueTrail()
    {
        while (rbStarship.velocity.sqrMagnitude > minSpeedTrail)
        {
            Vector2 point = transform.position;
            points.RemoveAt(0);
            points.Add(point);
            lr.SetPositions(points.ToArray());
            yield return null;
        }
        drawing = false;
        StartCoroutine(CleanTrail());
    }

    private IEnumerator CleanTrail()
    {
        while (!drawing && !(lr.positionCount == 0) && !(points.Count == 0))
        {
            points.RemoveAt(0);
            lr.positionCount--;
            lr.SetPositions(points.ToArray());
            yield return null;
        }
    }

}