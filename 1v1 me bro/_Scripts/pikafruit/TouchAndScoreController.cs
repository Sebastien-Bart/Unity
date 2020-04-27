using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TouchAndScoreController : MonoBehaviour
{

    public float scoreFadeSpeed;
    public static float timeOfRandomScore = 3f;

    public CameraShake cameraShake;
    public GameObject targetFruit;
    public FruitHolderController fruitHolder;
    public TextMeshProUGUI scoreText;

    public static bool canTouch = false;

    private int points = 0;


    public void OnTouch()
    {
        if (canTouch)
        {
            if (fruitHolder.actualFruit == targetFruit.GetComponent<Image>().sprite)
            {
                points++;
                targetFruit.GetComponent<TargetFruitController>().ReactToGoodAnswer();
                // particules bon point
            }
            else
            {
                points--;
                cameraShake.AskShake();
                // particules mauvais point
            }
        }
    }

    public void DisplayScore()
    {
        StartCoroutine("AnimationScore");
    }

    private IEnumerator AnimationScore()
    {
        yield return new WaitForSeconds(0.5f);
        scoreText.text = "0";
        while (scoreText.color != Color.black)
        {
            scoreText.color = Vector4.MoveTowards(scoreText.color, Color.black, scoreFadeSpeed * Time.deltaTime);
            yield return null;
        }
        float counter = 0f;
        while (counter < timeOfRandomScore)
        {
            int r = Random.Range(0, 99);
            scoreText.text = r.ToString();
            counter += Time.deltaTime;
            yield return null;
        }
        scoreText.text = points.ToString();
    }

}
