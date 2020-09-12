using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;

    public AudioClip clip;

    [HideInInspector]
    public string name;

    [Range(0f, 1f)] public float volume = 1;
    [Range(0f, 2f)] public float pitch = 0.3f;

    public void SetAudioSourceAttributes()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.clip = clip;
        name = clip.name;
    }

}
