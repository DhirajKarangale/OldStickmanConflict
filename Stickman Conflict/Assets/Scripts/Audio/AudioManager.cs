using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundsBG;
    public Sound[] sounds;
    private Sound selectedSound;
    private static int bgMusic = 0;

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

    private void Update()
    {
        if (!soundsBG[bgMusic].audioSource.isPlaying)
        {
            bgMusic = UnityEngine.Random.Range(0, soundsBG.Length);
            soundsBG[bgMusic].audioSource.Play();
        }
    }

    public void Play(string name)
    {
        selectedSound = Array.Find(sounds, sound => sound.name == name);
        // if (selectedSound.audioSource.isPlaying) selectedSound.audioSource.Stop();
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
                sound.audioSource.volume = Mathf.Clamp(sound.audioSource.volume, 0.08f, 0.5f);
                sound.audioSource.pitch = Mathf.Clamp(sound.audioSource.pitch, 0, 1);
            }
        }
    }

    public void StopBG()
    {
        foreach (Sound sound in soundsBG)
        {
            if (sound.audioSource.isPlaying)
            {
                sound.audioSource.Stop();
            }
        }
    }

    public void PauseBG()
    {
        foreach (Sound sound in soundsBG)
        {
            if (sound.audioSource.isPlaying)
            {
                sound.audioSource.Pause();
            }
        }
    }
}