using System.Collections;
using TMPro;
using UnityEngine;

public class StarshipPointManager : MonoBehaviour
{
    public int requiredPoints = 15;
    public int points { get; private set; } = 0;
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
            AudioManagerForOneGame.am.PlaySound("Win");
            if (transform.GetComponent<StarshipController>().spawnPos.x < 0)
                winMenu.SetWinner("left");
            else
                winMenu.SetWinner("right");
            StartCoroutine(WaitWinScreen());
        }
    }

    private IEnumerator WaitWinScreen()
    {
        yield return new WaitForSecondsRealtime(1f);
        winMenu.ActivateMenu();
    }

    public void RemovePoints(int toRemove)
    {
        points -= toRemove;
        pointsTxt.text = points.ToString();
    }
}
