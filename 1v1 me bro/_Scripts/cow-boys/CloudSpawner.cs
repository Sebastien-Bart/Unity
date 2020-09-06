using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float maxSpeed = 0.01f;
    public float minSpeed = 0.001f;
    public int maxNbClouds = 8;
    public int minNbClouds = 4;
    public Transform topLeftLimit, topRightLimit;
    public GameObject[] clouds_prefabs;

    private int nbClouds;
    private List<GameObject> clouds;

    void Start()
    {
        clouds = new List<GameObject>();
        createClouds();
    }

    private void createClouds()
    {
        nbClouds = Random.Range(minNbClouds, maxNbClouds);
        for (int i = 0; i < nbClouds; i++)
        {
            float speed = Random.Range(minSpeed, maxSpeed);
            Vector3 pos = new Vector3();
;           pos.x = Random.Range(topLeftLimit.position.x, topRightLimit.position.x);
            pos.y = Random.Range(topLeftLimit.position.y, topRightLimit.position.y);
            pos.z = 3;

            GameObject cloud = Instantiate(clouds_prefabs[(int)Random.Range(0, clouds_prefabs.Length)]);
            cloud.transform.position = pos;
            StartCoroutine(MakeCloudMove(cloud, speed));
            clouds.Add(cloud);
        }
    }

    private IEnumerator MakeCloudMove(GameObject cloud, float speed)
    {
        for (; ; )
        {
            if (!InGameMenuNew.Paused)
            {
                Vector3 newPos = Vector3.MoveTowards(cloud.transform.position, cloud.transform.position + Vector3.right, speed * Time.deltaTime);
                cloud.transform.position = newPos;
                yield return null;
            }
            else
                yield return null;
        }
    }

    public void Reset()
    {
        StopAllCoroutines();
        foreach (var item in clouds)
        {
            Destroy(item);
        }
        createClouds();
    }
}
