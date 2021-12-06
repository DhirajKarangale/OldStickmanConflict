using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
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
            sound.audioSource.loop = sound.loop;
        }
    }

    public void BGMod(string name, float volume, float pitch)
    {
        selectedSound = Array.Find(sounds, sound => sound.name == name);
        selectedSound.volume = volume;
        selectedSound.pitch = pitch;
    }

    public void Play(string name)
    {
        selectedSound = Array.Find(sounds, sound => sound.name == name);
        if (selectedSound.audioSource.isPlaying) selectedSound.audioSource.Stop();
        selectedSound.audioSource.Play();
    }

    public void Stop(string name)
    {
        selectedSound = Array.Find(sounds, sound => sound.name == name);
        selectedSound.audioSource.Stop();
    }
}