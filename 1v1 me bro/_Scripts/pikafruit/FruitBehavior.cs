using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    public static bool ending;

    private Vector3 initScale;

    private void Start()
    {
        initScale = transform.localScale;
    }

    void Update()
    {
        if (!ending)
        {
            CalculateScaleAccordingToPosition();
        }
    }

    public void CheckPosition()
    {
        if (transform.position.y <= -6.5f)
        {
            transform.position = new Vector3(transform.position.x, 6, transform.position.z);
        }
    }

    private void CalculateScaleAccordingToPosition()
    {
        float scaleY, scaleX;
        float yPos = transform.position.y;
        scaleY = initScale.y - (Mathf.Abs(yPos) / 6f) * initScale.y;
        scaleX = initScale.x - (Mathf.Abs(yPos) / 6f) * initScale.x;
        if (!(scaleX < 0) && !(scaleY < 0))
        {
            transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
        }
    }

}
