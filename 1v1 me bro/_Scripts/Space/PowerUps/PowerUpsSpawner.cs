using System.Collections;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    [Header("PowerUps & Stars")]
    public Transform collectiblesStarHolder;
    public GameObject collectitbleStar;
    public Transform powerUpsHolder;
    public GameObject[] powerUps;

    [Header ("Settings")]
    public int maxPowerUpsOnField;
    public float maxDelayPowerUp;
    public float minDelayPowerUp;
    public float maxDelayStarSpawn;
    public float minDelayStarSpawn;

    private float counter;
    private float w;
    private float h;
    

    void Start()
    {
        w = Camera.main.pixelWidth;
        h = Camera.main.pixelHeight;
        StartCoroutine(RandomlySpawnRandomPowerUp());
        StartCoroutine(SpawnCollectibleStarWhenNone());
    }

    private void SpawnPowerUp(GameObject toSpawn, Transform parent)
    {
        float offset = 50f;
        float x = Random.Range(offset, w - offset);
        float y = Random.Range(offset, h - offset);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 18));
        GameObject spawned = Instantiate(toSpawn, parent);
        spawned.transform.position = pos;
    }

    private IEnumerator RandomlySpawnRandomPowerUp()
    {
        float delay = Random.Range(minDelayPowerUp, maxDelayPowerUp);
        while (true)
        {
            counter += Time.deltaTime;
            if (counter >= delay && powerUpsHolder.childCount < maxPowerUpsOnField)
            {
                int r = Random.Range(0, powerUps.Length);
                SpawnPowerUp(powerUps[r], powerUpsHolder);
                counter = 0f;
                delay = Random.Range(minDelayPowerUp, maxDelayPowerUp);
            }
            else if (powerUpsHolder.childCount >= maxPowerUpsOnField)
            {
                counter = 0f;
            }
            yield return null;
        }
    }

    private IEnumerator SpawnCollectibleStarWhenNone()
    {
        while (true)
        {
            if (collectiblesStarHolder.childCount <= 1)
            {
                float r = Random.Range(minDelayStarSpawn, maxDelayStarSpawn);
                yield return new WaitForSeconds(r);
                SpawnPowerUp(collectitbleStar, collectiblesStarHolder);
            }
            yield return null;
        }
    }

}
