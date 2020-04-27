using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetFruitController : MonoBehaviour
{
    public TextMeshProUGUI endTextHolder;
    public Sprite[] fruitSprites;
    [Range(1f, 20f)] public float speed;

    private Vector3 initScale;
    private RectTransform rect;
    private bool running = false;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        initScale = rect.localScale;
    }

    public void ReactToGoodAnswer()
    {
        if (!running)
            StartCoroutine("ChangeFruit");
        else
        {
            StopCoroutine("ChangeFruit");
            StartCoroutine("ChangeFruit");
        }
    }

    public void End()
    {
        StopAllCoroutines();
        StartCoroutine("EndAnimation");
    }

    private IEnumerator EndAnimation()
    {
        while (rect.localScale != new Vector3(0f, 0f, initScale.z))
        {
            Vector3 newScale = Vector3.MoveTowards(rect.localScale, new Vector3(0f, 0f, initScale.z), speed * Time.deltaTime);
            rect.localScale = newScale;
            yield return null;
        }
        endTextHolder.color = Color.black;
        endTextHolder.fontSize = 80;
        endTextHolder.text = "Finished !";
    }

    private IEnumerator ChangeFruit()
    {
        running = true;
        while (rect.localScale != new Vector3(0f, 0f, initScale.z))
        {
            Vector3 newScale = Vector3.MoveTowards(rect.localScale, new Vector3(0f, 0f, initScale.z), speed * Time.deltaTime);
            rect.localScale = newScale;
            yield return null;
        }
        rect.GetComponent<Image>().sprite = fruitSprites[(int) Random.Range(0, fruitSprites.Length)];
        while (rect.localScale != initScale)
        {
            Vector3 newScale = Vector3.MoveTowards(rect.localScale, initScale, speed * Time.deltaTime);
            rect.localScale = newScale;
            yield return null;
        }
        running = false;
    }

}
