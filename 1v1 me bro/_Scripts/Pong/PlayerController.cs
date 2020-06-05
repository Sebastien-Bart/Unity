using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int points = 0;
    public float speed = 5f;

    private Rigidbody2D rb;
    private bool goUp = true;
    private bool moving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveWhenPressed()
    {
        moving = true;
    }

    public void ButtonReleased()
    {
        goUp = !goUp;
        moving = false;
    }

    private void FixedUpdate()
    {
        int inverse = 1;
        if (!goUp)
            inverse = -1;

        if (moving)
            rb.velocity = new Vector3(0f, inverse * speed * Time.deltaTime, 0f);
        else
            rb.velocity = Vector3.zero;
    }

    public void Goal()
    {
        points++;
    }

    public IEnumerator DisplayScore()
    {
        throw new System.NotImplementedException();
    }

}
