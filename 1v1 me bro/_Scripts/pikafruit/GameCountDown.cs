using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCountDown : MonoBehaviour
{
    public float time;
    public TargetFruitController targetFruitController;
    public FruitsRotation r1, r2;
    public TouchAndScoreController tc1, tc2;

    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = ((int)time).ToString("0.00");
    }


    public IEnumerator StartCountDown()
    {
        float counter = time;
        while (counter > 0f)
        {
            counter -= Time.deltaTime;
            if (counter < 0f)
                textMeshPro.text = "0.00";
            else
                textMeshPro.text = counter.ToString("0.00");
            yield return null;
        }
        EndTheGame();
    }

    private void EndTheGame()
    {
        TouchAndScoreController.canTouch = false;
        targetFruitController.End();
        r1.End();
        r2.End();
        tc1.DisplayScore();
        tc2.DisplayScore();
    }
    
}
