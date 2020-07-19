using UnityEngine;

public class StarRandomGeneration : MonoBehaviour
{
    public GameObject etoile1, etoile2;
    public int maxNbStars;
    public int minNbStars;

    private int nbStars;
    private Camera cam;

    void Start()
    {
        nbStars = Random.Range(minNbStars, maxNbStars);
        cam = Camera.main;

        float w = cam.pixelWidth;
        float h = cam.pixelHeight;

        for (int i = 0; i < nbStars; i++)
        {
            float x = Random.Range(0f, w);
            float y = Random.Range(0f, h);
            Vector3 pos = cam.ScreenToWorldPoint(new Vector3(x, y, 19));

            if (Random.Range(0f, 1f) < 0.5f) // etoile1
                Instantiate(etoile1, pos, Quaternion.identity, transform);
            else // etoile2
                Instantiate(etoile2, pos, Quaternion.identity, transform);
        }
    }

}
