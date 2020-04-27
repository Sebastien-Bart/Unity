using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHolderController : MonoBehaviour
{
    public Sprite actualFruit { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actualFruit = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
