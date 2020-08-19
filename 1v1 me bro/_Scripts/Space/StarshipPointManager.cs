using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarshipPointManager : MonoBehaviour
{
    public int requiredPoints = 15;

    private int points = 0;
    public int Points { get => points; }

    public WinMenu winMenu;

    public TextMeshProUGUI pointsTxt;


    private void Start()
    {
        pointsTxt.text = points.ToString();
    }

    public void AddPoints(int toAdd)
    {
        points += toAdd;
        pointsTxt.text = points.ToString();
        if (points >= requiredPoints)
        {
            winMenu.ActivateMenu();
        }
    }

    public void RemovePoints(int toRemove)
    {
        points -= toRemove;
        pointsTxt.text = points.ToString();
    }
}
