using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{ 
    public Sound[] sounds;
    [HideInInspector]
    public bool isMuted = false;
    private AudioSource audioS;

    private void Awake()
    {
        audioS = FindObjectOfType<AudioSource>();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("isMuted") == 1)
        {
            isMuted = true;
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        if (isMuted == true)
            return;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();


    }
    public void PlayCustom(AudioClip audio)
    {
        if (isMuted == true)
            return;
        audioS.PlayOneShot(audio, 1);
    }

}
