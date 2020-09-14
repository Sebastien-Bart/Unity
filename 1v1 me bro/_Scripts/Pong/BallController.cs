using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public WinMenu winMenu;

    [Header("CameraShake")]
    public CameraShake camShake;

    [Header("Players")]
    public PlayerController leftPlayer;
    public PlayerController rightPlayer;

    [Header("Ball Settings")]
    public ParticleSystem goalParticle;
    public ParticleSystem hitParticle;
    public float startSpeed;
    public float maxSpeedSqr;
    [Range(1f, 1.2f)] public float acceleration;
    [Range(1f, 10f)] public float shrinkSpeed;

    private Vector3 initSize;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 prevVel;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(KickOff());
        initSize = transform.localScale;
        prevVel = Vector2.zero;
    }

    public IEnumerator KickOff()
    {
        int x = 1;
        if (Random.Range(0,2) == 0)
            x = -1;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(x*startSpeed, 0);
        prevVel = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayHitParticleAccordingToCollission(collision);
        if (collision.collider.tag == "raquette")
        {
            AudioManagerForOneGame.am.PlaySound("pongHitPaddle");
            camShake.AskLittleShake();
            CalculateNewTrajectory(collision);
        }
        else if (collision.collider.tag == "but")
        {
            AudioManagerForOneGame.am.PlaySound("pongHitWall");
            camShake.AskShake();
            rb.velocity = Vector2.zero;
            StartCoroutine("Goal");
        }
        else
            AudioManagerForOneGame.am.PlaySound("pongHitWall");
    }

    private void PlayHitParticleAccordingToCollission(Collision2D collision)
    {
        ParticleSystem particle = Instantiate(hitParticle);
        Vector2 contactPos = collision.GetContact(0).point;
        particle.transform.position = contactPos;

        float xMin = contactPos.x - 0.2f;
        float xMax = contactPos.x + 0.2f;

        if (transform.position.x < xMax && transform.position.x > xMin) // collision sur mur haut ou bas
        {
            if (transform.position.y < contactPos.y)
                particle.transform.localRotation = Quaternion.Euler(-270, 0, 0);
            else if (transform.position.y > contactPos.y)
                particle.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }
        else
        {
            if (transform.position.x > contactPos.x)
                particle.transform.localRotation = Quaternion.Euler(0, 90, 0);
            if (transform.position.x < contactPos.x)
                particle.transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
        particle.Play();
    }

    private void CalculateNewTrajectory(Collision2D collision)
    {
        float nextMagnitudeSqr;

        if (prevVel.sqrMagnitude > rb.velocity.sqrMagnitude) // si coin de raquette
            nextMagnitudeSqr = (prevVel * acceleration).sqrMagnitude;
        else
            nextMagnitudeSqr = (rb.velocity * acceleration).sqrMagnitude;

        if (nextMagnitudeSqr > maxSpeedSqr)
            nextMagnitudeSqr = maxSpeedSqr;
            
        float tranche = collision.collider.transform.localScale.y / 8;
        float segment = Mathf.RoundToInt((transform.position.y - collision.collider.transform.position.y) / tranche);

        int xDirection = 1;
        int yDirection = 1;
        if (segment < 0)
            yDirection = -1;
        if (transform.position.x > 0)
            xDirection = -1;

        float angle = 0;
        if (Mathf.Abs(segment) == 1)
            angle = 15;
        else if (Mathf.Abs(segment) == 2)
            angle = 30;
        else if (Mathf.Abs(segment) >= 3)
            angle = 45;

        float xVel = xDirection * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Sqrt(nextMagnitudeSqr);
        float yVel = yDirection * Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Sqrt(nextMagnitudeSqr);

        rb.velocity = new Vector2(xVel, yVel);
        prevVel = rb.velocity;
    }

    private bool GoalCoroutinerunning = false;

    private IEnumerator Goal()
    {
        if (!GoalCoroutinerunning)
        {
            GoalCoroutinerunning = true;
            yield return new WaitForSeconds(0.5f);
            AudioManagerForOneGame.am.PlaySound("ballShrink");
            while (transform.localScale != Vector3.zero)
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(0.25f);
            AudioManagerForOneGame.am.PlaySound("pongExplosion");
            ParticleSystem goalP = Instantiate(goalParticle);
            goalP.transform.position = transform.position;
            goalP.Play();
            camShake.AskShake();
            yield return new WaitForSeconds(1f);

            if (transform.position.x > 0)
                leftPlayer.Goal();
            else if (transform.position.x < 0)
                rightPlayer.Goal();

            yield return new WaitForSeconds(2.5f); // Delai a voir selon coroutine displayscore
            // Verifier si partie n'est pas finie
            if (leftPlayer.points == 5 || rightPlayer.points == 5)
            {
                StartCoroutine("EndGame");
                yield break;
            }
            transform.position = Vector2.zero;
            AudioManagerForOneGame.am.PlaySound("ballRespawn");
            while (transform.localScale != initSize)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, initSize, shrinkSpeed * Time.deltaTime);
                yield return null;
            }
            StartCoroutine(KickOff());
        }
        GoalCoroutinerunning = false;
    }

    private IEnumerator EndGame()
    {
        AudioManagerForOneGame.am.PlaySound("End");
        if (rightPlayer.points > leftPlayer.points)
            winMenu.SetWinner("right");
        else
            winMenu.SetWinner("left");

        yield return new WaitForSeconds(1f);
        winMenu.ActivateMenu();
    }

}
