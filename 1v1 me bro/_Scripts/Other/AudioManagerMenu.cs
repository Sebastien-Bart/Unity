using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{

    public static AudioManager am;

    private void Awake()
    {
        if (am == null)
            am = GetComponent<AudioManager>();
        else
            Destroy(gameObject);
    }

}
