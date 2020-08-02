using System.Collections;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    public Transform powerUpsHolder;
    public GameObject[] powerUps;

    [Header ("Settings")]
    public int maxPowerUpsOnField;
    public float maxDelay;
    public float minDelay;

    private float counter;
    private float w;
    private float h;
    

    void Start()
    {
        w = Camera.main.pixelWidth;
        h = Camera.main.pixelHeight;
        StartCoroutine(RandomlySpawnRandomPowerUp());
    }

    private void SpawnPowerUp()
    {
        float x = Random.Range(0f, w - 10);
        float y = Random.Range(0f, h - 10);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 19));
        int r = Random.Range(0, powerUps.Length);
        GameObject powerUp = Instantiate(powerUps[r], powerUpsHolder);
        powerUp.transform.position = pos;
    }

    private IEnumerator RandomlySpawnRandomPowerUp()
    {
        float delay = Random.Range(minDelay, maxDelay);
        while (true)
        {
            counter += Time.deltaTime;
            if (counter >= delay && powerUpsHolder.childCount < maxPowerUpsOnField)
            {
                SpawnPowerUp();
                counter = 0f;
                delay = Random.Range(minDelay, maxDelay);
            }
            else if (powerUpsHolder.childCount >= maxPowerUpsOnField)
            {
                counter = 0f;
            }
            yield return null;
        }
    }

}
