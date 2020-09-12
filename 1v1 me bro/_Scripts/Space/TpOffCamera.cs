using UnityEngine;

public class TpOffCamera : MonoBehaviour
{

    private float minXcam;
    private float maxXcam;
    private float minYcam;
    private float maxYcam;

    void Start()
    {
        Camera cam = Camera.main;
        minXcam = cam.ScreenToWorldPoint(Vector3.zero).x;
        maxXcam = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f, 0f)).x;
        minYcam = cam.ScreenToWorldPoint(Vector3.zero).y;
        maxYcam = cam.ScreenToWorldPoint(new Vector3(0f, cam.pixelHeight, 0f)).y;
    }

    public void TPWhenOffCamera(Transform toMove)
    {
        if (toMove.position.x < minXcam)
        {
            toMove.position = new Vector3(maxXcam, toMove.position.y, toMove.position.z);
            AudioManagerForOneGame.am.PlaySound("Teleport");
        }
        else if (toMove.position.x > maxXcam)
        {
            toMove.position = new Vector3(minXcam, toMove.position.y, toMove.position.z);
            AudioManagerForOneGame.am.PlaySound("Teleport");
        }
        else if (toMove.position.y < minYcam)
        {
            toMove.position = new Vector3(toMove.position.x, maxYcam, toMove.position.z);
            AudioManagerForOneGame.am.PlaySound("Teleport");
        }
        else if (toMove.position.y > maxYcam)
        {
            toMove.position = new Vector3(toMove.position.x, minYcam, toMove.position.z);
            AudioManagerForOneGame.am.PlaySound("Teleport");
        }
    }

}
