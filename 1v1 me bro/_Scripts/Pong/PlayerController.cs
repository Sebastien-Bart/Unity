using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public ScoreDisplayManager scoreDM;
    public int points = 0;

    [Header("Settings")]
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
        if (!AbstractMenu.Paused)
            moving = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    public void ButtonReleased()
    {
        goUp = !goUp;
        moving = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
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
        scoreDM.StartCoroutine("DisplayAndUpdateScore");
    }

}
