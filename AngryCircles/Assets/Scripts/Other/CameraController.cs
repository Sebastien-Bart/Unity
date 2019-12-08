using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform circle, rightLimit, leftLimit;

    private Vector3 currentPosition;

    private void Start()
    {
        currentPosition = transform.position;
    }

    private void LateUpdate()
    {
        currentPosition.x = Mathf.Clamp(circle.position.x, leftLimit.position.x, rightLimit.position.x);
        transform.position = currentPosition;
    }
}
