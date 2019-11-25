using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch = 1f;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BG");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s.source.isPlaying) return true;

        return false;
    }

    public IEnumerator PlaySlapSounds(string name, string name2)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        yield return new WaitForSeconds(s.clip.length);
        s = Array.Find(sounds, sound => sound.name == name2);
        s.source.Play();
    }

    public void PlayCoroutine()
    {
        StartCoroutine(PlaySlapSounds("Slap","Oh!"));
    }
}
