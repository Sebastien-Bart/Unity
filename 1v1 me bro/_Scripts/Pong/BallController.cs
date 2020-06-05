using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditorInternal;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Players")]
    public PlayerController leftPlayer;
    public PlayerController rightPlayer;

    [Header("Ball Settings")]
    public float speed;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        KickOff();
    }

    public void KickOff()
    {
        float x = Random.Range(3f, speed - 0.5f);
        float y = Mathf.Sqrt(speed * speed - x * x);
        if ((int)Random.Range(0f, 2f) == 1)
            x *= -1;
        if ((int)Random.Range(0f, 2f) == 1)
            y *= -1;
        rb.velocity = new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "raquette")
        {
            // float tranche = collision.collider.transform.localScale.y / 8;
            // float segment = Mathf.RoundToInt((transform.position.y - collision.collider.transform.position.y) / tranche);
            // Debug.Log(segment);
            rb.velocity *= 1.20f;
        }

        else if (collision.collider.tag == "but")
        {
            rb.velocity = Vector2.zero;
            StartCoroutine("Goal");
        }
    }   

    private IEnumerator Goal()
    {
        while (sr.color != Color.red)
        {
            sr.color = Vector4.MoveTowards(sr.color, Color.red, 1f * Time.deltaTime);
            yield return null;
        }
        // Jouer particules but (Explosion) ? + cacher la balle
        yield return new WaitForSeconds(1f);

        if (transform.position.x > 0)
            leftPlayer.Goal();
        else if (transform.position.x < 0)
            rightPlayer.Goal();
        leftPlayer.StartCoroutine("DisplayScore");
        rightPlayer.StartCoroutine("DisplayScore");

        yield return new WaitForSeconds(3f); // Delai a voir selon coroutine displayscore
        // Verifier si partie n'est pas finie
        if (leftPlayer.points == 5 || rightPlayer.points == 5)
        {
            StartCoroutine("EndGame");
            yield break;
        }
        transform.position = Vector2.zero;
        sr.color = Color.white;
        yield return new WaitForSeconds(3f);
        KickOff();
    }

    private IEnumerator EndGame()
    {
        throw new System.NotImplementedException();
    }

}
