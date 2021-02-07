using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float lifetime;
    public float startShrink;

    private float current;

    private void Start()
    {
        current = 0f;
    }

    void Update()
    {
        current += Time.deltaTime;
        if (current > startShrink)
        {
            StartCoroutine(Shrink());
        }
    }

    private IEnumerator Shrink()
    {
        Vector3 initSize = transform.localScale;
        float speed = Vector3.Distance(initSize, Vector3.zero) / (lifetime - startShrink);
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

}
