using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundsBG;
    public Sound[] sounds;
    private Sound selectedSound;

    public static AudioManager instance = null;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
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

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
        }

        foreach (Sound sound in soundsBG)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = true;
        }
    }

    public void Play(string name)
    {
        
        selectedSound = Array.Find(sounds, sound => sound.name == name);
        if(selectedSound == null) selectedSound = Array.Find(soundsBG, sound => sound.name == name);
        if (selectedSound.audioSource.isPlaying) selectedSound.audioSource.Stop();
        selectedSound.audioSource.Play();
    }

    public void ModBG(float volume, float pitch)
    {
        foreach (Sound sound in soundsBG)
        {
            if (sound.audioSource.isPlaying)
            {
                sound.audioSource.volume = volume;
                sound.audioSource.pitch = pitch;
            }
        }
    }

    public void StopBG()
    {
        foreach (Sound sound in soundsBG)
        {
            if (sound.audioSource.isPlaying)
                sound.audioSource.Stop();
        }
    }

    public void PauseBG()
    {
        foreach (Sound sound in soundsBG)
        {
            if (sound.audioSource.isPlaying)
                sound.audioSource.Pause();
        }
    }
}