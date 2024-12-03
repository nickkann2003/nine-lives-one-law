using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public float volume = 1f;

    [SerializeField]
    private List<SoundEffect> soundEffects;

    private Dictionary<string, SoundEffect> sortedSounds = new Dictionary<string, SoundEffect>();

    [SerializeField]
    private List<MusicClip> music = new List<MusicClip>();
    private AudioClip selectedMusic;
    private bool mChanged = false;

    private Dictionary<string, MusicClip> sortedMusic = new Dictionary<string, MusicClip>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource.volume = volume;
        foreach(SoundEffect s in soundEffects)
        {
            sortedSounds.Add(s.name, s);
        }
    }

    private void Update()
    {
        if (mChanged)
        {
            audioSource.Stop();
            if(selectedMusic != null)
            {
                audioSource.clip = selectedMusic;
                audioSource.Play();
            }
            mChanged = false;
        }
    }

    /// <summary>
    /// Plays a sound at a given index in the sound effects list
    /// </summary>
    /// <param name="i"></param>
    public void PlaySound(int i)
    {
        soundEffects[i].PlaySound(audioSource);
    }

    /// <summary>
    /// Searches through sound effects and plays the first one with a matching name
    /// </summary>
    /// <param name="name">Name of the sound to be played</param>
    public void PlaySound(String name)
    {
        if (sortedSounds.ContainsKey(name))
        {
            sortedSounds[name].PlaySound(audioSource);
        }
    }

    public void SetMusic(string musicName)
    {
        // Music found in list
        if (sortedMusic.ContainsKey(musicName))
        {
            AudioClip c = sortedMusic[musicName].music;
            if(selectedMusic != c)
            {
                mChanged = true;
                selectedMusic = c;
            }
            return;
        }

        // Music not found, stop playing
        mChanged = true;
        selectedMusic = null;
    }
}

[Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
    public float volume = 1f;

    public void PlaySound(AudioSource source)
    {
        source.PlayOneShot(clip, volume);
    }
}

/// <summary>
/// Pure fabrication for serializing music with a name
/// </summary>
[Serializable]
public class MusicClip
{
    public string name;
    public AudioClip music;
}