using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountDown : MonoBehaviour
{
    public TargetFruitController targetFruitController;
    public GameCountDown gameCountDown;
    public FruitsRotation r1, r2;

    private TextMeshProUGUI startCountDownTxt;

    void Start()
    {
        startCountDownTxt = GetComponent<TextMeshProUGUI>();
        StartCoroutine(begin());
    }

    private IEnumerator begin()
    {
        startCountDownTxt.text = "3";
        yield return new WaitForSeconds(1f);
        startCountDownTxt.text = "2";
        yield return new WaitForSeconds(1f);
        startCountDownTxt.text = "1";
        yield return new WaitForSeconds(1f);
        startCountDownTxt.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        while (startCountDownTxt.color != Color.white)
        {
            startCountDownTxt.color = Vector4.MoveTowards(startCountDownTxt.color, Color.white, 3f * Time.deltaTime);
            yield return null;
        }
        startCountDownTxt.text = null;
        TouchAndScoreController.canTouch = true;
        targetFruitController.ReactToGoodAnswer();
        r1.MakeAllFruitsMove();
        r2.MakeAllFruitsMove();
        StartCoroutine(gameCountDown.StartCountDown());
    }

}
