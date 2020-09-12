using UnityEngine;

public class AudioManagerForOneGame : MonoBehaviour
{
    public static AudioManager am;

    private void Awake()
    {
        am = GetComponent<AudioManager>();
    }
}
