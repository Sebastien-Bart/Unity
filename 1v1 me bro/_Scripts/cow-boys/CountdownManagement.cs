using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManagement : MonoBehaviour
{
    [Range(0f, 5f)] public float moveSpeed = 2f;
    [Range(0f, 10f)] public float maxDelayCountDown = 5f;
    public shootController shootController1, shootController2;
    public Sprite[] sprites;

    private RectTransform rect;

    private Vector3 initPosition;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        StartCoroutine("MoveDownAndStartCountDown");
        initPosition = rect.anchoredPosition;
    }

    public IEnumerator MoveDownAndStartCountDown()
    {
        Vector3 goal = Vector3.zero;
        while (rect.anchoredPosition.y != 0)
        {
            if (!AbstractMenu.Paused)
            {
                Vector2 newPosition = Vector3.MoveTowards(rect.anchoredPosition, goal, moveSpeed);
                rect.anchoredPosition = newPosition;
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
        StartCoroutine("CountDown");
    }

    public IEnumerator MoveUp()
    {
        Vector3 goal = initPosition;
        while (rect.anchoredPosition.y != initPosition.y)
        {
            if (!AbstractMenu.Paused)
            {
                Vector2 newPosition = Vector3.MoveTowards(rect.anchoredPosition, goal, moveSpeed);
                rect.anchoredPosition = newPosition;
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator CountDown()
    {
        image.sprite = sprites[0];
        yield return new WaitForSeconds(1f);
        AudioManagerForOneGame.am.PlaySound("panneauChange");
        image.sprite = sprites[3];
        yield return new WaitForSeconds(1f);
        AudioManagerForOneGame.am.PlaySound("panneauChange");
        image.sprite = sprites[2];
        yield return new WaitForSeconds(1f);
        AudioManagerForOneGame.am.PlaySound("panneauChange");
        image.sprite = sprites[1];
        yield return new WaitForSeconds(1f);
        image.sprite = sprites[4];
        float random = Random.Range(0, maxDelayCountDown);
        Debug.Log("feu dans : " + random);
        yield return new WaitForSeconds(random);
        AudioManagerForOneGame.am.PlaySound("panneauFire");
        image.sprite = sprites[5];
        shootController1.goodShot = true;
        shootController2.goodShot = true;
    }

    private void Reset()
    {
        rect.anchoredPosition = initPosition;
        image.sprite = sprites[0];
        StopAllCoroutines();
        shootController1.goodShot = false;
        shootController2.goodShot = false;
    }

}
