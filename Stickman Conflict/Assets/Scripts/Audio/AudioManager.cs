﻿using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] soundsBG;
    public Sound[] sounds;
    private Sound selectedSound;
    private static int bgMusic = 0;

    private void Awake()
    {
        MakeSingleton();

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.playOnAwake = false;
        }

        foreach (Sound sound in soundsBG)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.playOnAwake = false;
        }
    }

    private void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (!soundsBG[bgMusic].audioSource.isPlaying)
        {
            bgMusic = UnityEngine.Random.Range(0, soundsBG.Length);
            soundsBG[bgMusic].audioSource.Play();
            return;
        }

        for (int i = 0; i < soundsBG.Length; i++)
        {
            if (soundsBG[i].audioSource.isPlaying && (i != bgMusic))
            {
                soundsBG[i].audioSource.Stop();
            }
        }
    }

    public void Play(string name)
    {
        selectedSound = Array.Find(sounds, sound => sound.name == name);
        // if (selectedSound.audioSource.isPlaying) selectedSound.audioSource.Stop();
        selectedSound.audioSource.Play();
    }

    public void ModBG(float volume)
    {
        foreach (Sound sound in soundsBG)
        {
            sound.audioSource.volume = volume;
        }
    }

    public void StopBG()
    {
        foreach (Sound sound in soundsBG)
        {
            sound.audioSource.Stop();
        }
    }

    public void PauseBG()
    {
        foreach (Sound sound in soundsBG)
        {
            sound.audioSource.Pause();
        }
    }
}