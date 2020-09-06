using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColorBroCoinBtn : MonoBehaviour
{
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(Random.Range(0, 0.6f), Random.Range(0, 0.6f), Random.Range(0, 0.6f), 1f);
        StartCoroutine(RandomColor());
    }

    private IEnumerator RandomColor()
    {
        Color randomColor = new Color(Random.Range(0, 0.6f), Random.Range(0, 0.6f), Random.Range(0, 0.6f), 1f);
        while (true)
        {
            if (img.color == randomColor)
            {
                randomColor = new Color(Random.Range(0, 0.8f), Random.Range(0, 0.8f), Random.Range(0, 0.8f), 1);
            }
            Vector4 step = Vector4.MoveTowards(img.color, randomColor, Time.deltaTime / 3f);
            img.color = step;
            yield return null;
        }
    }
}
