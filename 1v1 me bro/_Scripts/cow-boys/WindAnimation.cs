using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAnimation : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("empty"))
        {
            Destroy(gameObject);
        }
    }
}
