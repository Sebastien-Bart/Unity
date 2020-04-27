using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public string characterName;

    [SerializeField] [Range(0f, 20f)] private float maxDelayBlink = 16f;

    private Animator animator;
    private bool blinking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("RandomlyPlayBlink");
    }

    private void Reset()
    {
        animator.Play(characterName + "_idle");
    }

    private void Update()
    {
        if (blinking)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(characterName + "_blink"))
            {
                blinking = false;
                animator.SetBool("blinking", false);
            }
        }
    }

    private IEnumerator RandomlyPlayBlink()
    {
        for (; ; )
        {
            float random = Random.Range(0f, maxDelayBlink);
            float counter = 0f;
            while (counter < random)
            {
                counter += Time.deltaTime;
                yield return null;
            }
            blinking = true;
            animator.SetBool("blinking", true);
        }
    }

    public void PlayShootAnimation(bool goodShot)
    {
        animator.Play(characterName + "_shoot", 0);
    }

    public void PlayHurtAnimation()
    {
        animator.Play(characterName + "_hurt", 0);
    }

    public void PlayRoundVictoryAnimation()
    {
        StopPlayingReload();
        animator.SetBool("round_victory", true);
    }

    public void StartPlayingReload()
    {
        animator.SetBool("reloading", true);
    }

    public void StopPlayingReload()
    {
        animator.SetBool("reloading", false);
    }

    public void PlayDead()
    {
        animator.Play(characterName + "_dead");
    }

}
