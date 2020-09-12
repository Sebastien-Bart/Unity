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
    public static float timeOfRandomScore = 2f;

    [Header("Other Settings")]
    public CameraShake cameraShake;
    public GameObject targetFruit;
    public FruitHolderController fruitHolder;
    public TextMeshProUGUI scoreText;
    public WinMenu winMenu;

    public static bool canTouch = false;

    private bool colorChanging = false;
    private Coroutine colorCouroutine;

    public int Points { get; private set; } = 0;

    public void OnTouch()
    {
        if (canTouch && !InGameMenuNew.Paused)
        {
            if (fruitHolder.actualFruit == targetFruit.GetComponent<Image>().sprite)
            {
                Points++;
                AudioManagerForOneGame.am.PlaySound("goodChoice");
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
                AudioManagerForOneGame.am.PlaySound("badChoice");
                Points--;
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
        AudioManagerForOneGame.am.PlaySound("DisplayScore");
        scoreText.text = Points.ToString();
        yield return new WaitForSeconds(2f);

        bool rightPlayer = transform.position.x > 0;
        if (Points > otherTouchAndScoreController.Points && rightPlayer)
        {
            winMenu.SetWinner("right");
        }
        else if (Points == otherTouchAndScoreController.Points)
        {
            winMenu.SetWinner("tie");
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
