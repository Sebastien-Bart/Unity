using UnityEngine;

public class RandomBackgroundColor : MonoBehaviour
{
    public SpriteRenderer backgroundSR;

    private Camera maincam;
    private Color randomColor;

    void Start()
    {
        randomColor = new Color(Random.Range(0, 0.25f), Random.Range(0, 0.25f), Random.Range(0, 0.25f), 1);
        maincam = Camera.main;
        int r = Random.Range(8, 34);
        int g = Random.Range(8, 34);
        int b = Random.Range(8, 34);
        Color32 startColor = new Color32((byte)r, (byte)g, (byte)b, 255);
        maincam.backgroundColor = startColor;
        backgroundSR.color = new Color32((byte)(r + 20), (byte)(g + 20), (byte)(b + 20), 255);
    }

    private void Update()
    {
        if (maincam.backgroundColor == randomColor)
        {
            randomColor = new Color(Random.Range(0, 0.8f), Random.Range(0, 0.8f), Random.Range(0, 0.8f), 1);
        }
        Vector4 step = Vector4.MoveTowards(maincam.backgroundColor, randomColor, Time.deltaTime/15f);
        maincam.backgroundColor = step;
        backgroundSR.color = (Vector4)maincam.backgroundColor + new Vector4(0.2f, 0.2f, 0.2f);
    }

}
