using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayManager : MonoBehaviour
{
    [Header("Settings")]
    public float speed;

    [Header ("Player Scripts")]
    public PlayerController leftPlayer;
    public PlayerController rightPlayer;

    [Header("Player ScoreHolders Texts")]
    public TextMeshProUGUI leftPlayerScoreTxt; 
    public TextMeshProUGUI rightPlayerScoreTxt;

    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public IEnumerator DisplayAndUpdateScore()
    {
        Vector2 initPos = rect.anchoredPosition;
        Vector2 displayPos = new Vector2(0, -88);
        while (rect.anchoredPosition != displayPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, displayPos, speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Camera.main.GetComponent<AudioManager>().PlaySound("updatePoint");
        rightPlayerScoreTxt.text = rightPlayer.points.ToString();
        leftPlayerScoreTxt.text = leftPlayer.points.ToString();
        yield return new WaitForSeconds(1f);
        while (rect.anchoredPosition != initPos)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, initPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
