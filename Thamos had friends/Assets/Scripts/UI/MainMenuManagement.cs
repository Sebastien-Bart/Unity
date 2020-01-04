using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManagement : MonoBehaviour
{

    [SerializeField] private Transform bg;
    [SerializeField] private float bgSpeed;

    private LineRenderer lr;

    private int actualFocus = 0;
    private int prevFocus = 1;

    void Start()
    {
        // line renderer
        Vector3[] corners = new Vector3[4];
        lr = GetComponent<LineRenderer>();
        transform.GetComponent<RectTransform>().GetWorldCorners(corners);
        lr.SetPosition(0, corners[0]);
        lr.SetPosition(1, corners[1]);
        lr.SetPosition(2, corners[2]);
        lr.SetPosition(3, corners[3]);
        lr.loop = true;

        // bg
        bg.position = transform.GetChild(0).position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(actualFocus).GetComponent<Button>().onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!(actualFocus <= 0))
                actualFocus--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!(actualFocus >= transform.childCount - 1))
                actualFocus++;
        }

        if (actualFocus != prevFocus)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == actualFocus)
                {
                    transform.GetChild(i).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1);
                }
                else
                {
                    transform.GetChild(i).GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0);
                }
            }
        }

        prevFocus = actualFocus;

        Vector3 bgMov = Vector3.MoveTowards(bg.transform.position, transform.GetChild(actualFocus).position, bgSpeed * Time.deltaTime);
        bgMov.z = bg.position.z;
        bg.position = bgMov;
    }

    public void resume()
    {
        Debug.Log("Resume pressed");
    }

    public void levelSelection()
    {
        Debug.Log("Level Selection pressed");
    }

    public void newGame()
    {
        Debug.Log("New Game pressed");
    }

    public void settings()
    {
        Debug.Log("Settings pressed");
    }

}
