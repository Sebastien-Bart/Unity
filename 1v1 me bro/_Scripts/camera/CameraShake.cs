using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float maxOffset = 0.5f;

    private Vector3 initPos;
    private bool running = false;
    private Coroutine coroutine = null;

    void Start()
    {
        initPos = transform.position;
    }

    public void AskShake()
    {
        if (running)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Shake(false));
    }

    public void AskLittleShake()
    {
        if (running)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Shake(true));
    }

    private IEnumerator Shake(bool isLittleShake)
    {
        running = true;
        float x, y;
        if (Random.value < 0.5f)
            x = Random.Range(-maxOffset, -0.2f);
        else
            x = Random.Range(0.2f, maxOffset);
        if (Random.value < 0.5f)
            y = Random.Range(-maxOffset, -0.2f);
        else
            y = Random.Range(0.2f, maxOffset);
        if (isLittleShake)
        {
            x /= 3f;
            y /= 3f;
        }
        Vector3 goal = new Vector3(initPos.x + x, initPos.y + y, initPos.z);
        Vector3 newPos;
        while (transform.position != goal)
        {
            newPos = Vector3.MoveTowards(transform.position, goal, 0.2f);
            newPos.z = initPos.z;
            transform.position = newPos;
            yield return null;
        }
        while (transform.position != initPos)
        {
            newPos = Vector3.MoveTowards(transform.position, initPos, 0.05f);
            newPos.z = initPos.z;
            transform.position = newPos;
            yield return null;
        }
        running = false;
    }

}
