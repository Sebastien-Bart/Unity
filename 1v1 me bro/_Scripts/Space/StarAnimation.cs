using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public float maxDelayAnim;

    private float delayAnim;
    private float counter;
    private Animator animator;

    void Start()
    {
        counter = 0f;
        delayAnim = Random.Range(0f, maxDelayAnim);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (counter >= delayAnim)
        {
            animator.Play("blink");
            counter = 0f;
            delayAnim = Random.Range(0f, maxDelayAnim);
        }
        counter += Time.deltaTime;
    }

}
