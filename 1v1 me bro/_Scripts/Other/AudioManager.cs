using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool dontDestroyOnLoad = false;

    public Sound[] sounds;

    private void Awake()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.SetAudioSourceAttributes();
        }
    }

    public void PlaySound(string name)
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.Log("AudioManager : sound '" + name + "' not found !");
                return;
            }
            s.source.PlayOneShot(s.clip);
        }
    }

    public void PlayMainTheme()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "MainTheme");
        if (PlayerPrefs.GetInt("music", 1) == 0) 
        {
            s.source.volume = 0;
        }
        s.source.loop = true;
        s.source.Play();
    }

}
