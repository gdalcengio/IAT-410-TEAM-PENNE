using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;
    private bool bossEnter = false;
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

    void Update() {

        if (!bossEnter && SceneManager.GetActiveScene().buildIndex == 7) {
            bossEnter = true;
            FindObjectOfType<AudioManager>().Play("Boss");
            FindObjectOfType<AudioManager>().Stop("BG");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
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
