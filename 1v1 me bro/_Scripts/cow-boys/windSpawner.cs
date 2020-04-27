using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windSpawner : MonoBehaviour
{

    public Camera m_camera;
    public GameObject wind;

    [SerializeField] [Range(0.1f, 15f)] private float maxDelay = 5f;
    void Start()
    {
        StartCoroutine(RandomlySpawnWind());
    }

    IEnumerator RandomlySpawnWind()
    {
        for (; ; )
        {
            Vector3 randomPos = m_camera.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
            randomPos.z = -1;
            float counter = 0f;
            float random = Random.Range(0, maxDelay);

            while (counter < random)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            Instantiate(wind, randomPos, Quaternion.identity);
        }
    }

}
