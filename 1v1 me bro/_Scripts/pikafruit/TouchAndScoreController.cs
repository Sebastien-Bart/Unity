using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TouchAndScoreController : MonoBehaviour
{
    [Header("OtherTouchAndScoreController")]
    public TouchAndScoreController otherTouchAndScoreController;

    [Header("Settings")]
    public SpriteRenderer colorAnswer;
    public float colorAnswerSpeed;
    public float scoreFadeSpeed;
    public static float timeOfRandomScore = 3f;

    [Header("Other Settings")]
    public CameraShake cameraShake;
    public GameObject targetFruit;
    public FruitHolderController fruitHolder;
    public TextMeshProUGUI scoreText;
    public WinMenu winMenu;

    public static bool canTouch = false;

    private bool colorChanging = false;
    private Coroutine colorCouroutine;
    
    private int points = 0;
    public int Points { get => points;}

    public void OnTouch()
    {
        if (canTouch)
        {
            if (fruitHolder.actualFruit == targetFruit.GetComponent<Image>().sprite)
            {
                points++;
                targetFruit.GetComponent<TargetFruitController>().ReactToGoodAnswer();
                if (!colorChanging)
                    colorCouroutine = StartCoroutine(changeColorAnswer(new Color32(134, 255, 151, 255)));
                else
                {
                    StopCoroutine(colorCouroutine);
                    colorCouroutine = StartCoroutine(changeColorAnswer(new Color32(134, 255, 151, 255)));
                }
            }
            else
            {
                points--;
                cameraShake.AskShake();
                if (!colorChanging)
                    colorCouroutine = StartCoroutine(changeColorAnswer(new Color32(255, 121, 121, 255)));
                else
                {
                    StopCoroutine(colorCouroutine);
                    colorCouroutine = StartCoroutine(changeColorAnswer(new Color32(255, 121, 121, 255)));
                }
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
        yield return new WaitForSeconds(2f);

        bool rightPlayer = transform.position.x > 0;
        if (points > otherTouchAndScoreController.Points && rightPlayer)
        {
            winMenu.SetWinner("right");
        }
        else
        {
            winMenu.SetWinner("left");
        }
        winMenu.ActivateMenu();
    }

    private IEnumerator changeColorAnswer(Color targetColor)
    {
        colorChanging = true;
        while (colorAnswer.color != targetColor)
        {
            colorAnswer.color = Vector4.MoveTowards(colorAnswer.color, targetColor, colorAnswerSpeed * Time.deltaTime);
            yield return null;
        }
        while (colorAnswer.color != new Color(1, 1, 1, 0))
        {
            colorAnswer.color = Vector4.MoveTowards(colorAnswer.color, new Color(1, 1, 1, 0), colorAnswerSpeed * Time.deltaTime);
            yield return null;
        }
        colorChanging = false;
    }

}
