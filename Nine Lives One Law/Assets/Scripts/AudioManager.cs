using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public float volume = 1f;

    int sourceIndex = 0;
    public List<AudioSource> sources = new List<AudioSource>();

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
        foreach(SoundEffect s in soundEffects)
        {
            sortedSounds.Add(s.name, s);
        }
        foreach(MusicClip m in music)
        {
            sortedMusic.Add(m.name, m);
        }
        for(int i = 0; i < 10; i++)
        {
            sources.Add(gameObject.AddComponent<AudioSource>());
            sources[i].volume = volume;
        }
        audioSource.volume = volume;
        audioSource.loop = true;
    }

    private void Update()
    {
        if (mChanged)
        {
            audioSource.Pause();
            audioSource.clip = selectedMusic;
            if(selectedMusic != null)
            {
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
        soundEffects[i].PlaySound(sources[sourceIndex], volume);
        sourceIndex = (sourceIndex + 1)%10;
    }

    /// <summary>
    /// Searches through sound effects and plays the first one with a matching name
    /// </summary>
    /// <param name="name">Name of the sound to be played</param>
    public void PlaySound(String name)
    {
        if (sortedSounds.ContainsKey(name))
        {
            sortedSounds[name].PlaySound(sources[sourceIndex], volume);
            sourceIndex = (sourceIndex + 1) % 10;
        }
    }

    /// <summary>
    /// Searches through sound effects and plays the first one with a matching name
    /// </summary>
    /// <param name="name">Name of the sound to be played</param>
    public void PlaySound(String name, AudioSource source)
    {
        if (sortedSounds.ContainsKey(name))
        {
            sortedSounds[name].PlaySound(source, volume);
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

    public void SetVolume(float vol)
    {
        this.volume = vol;
    }
}

[Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
    public float volume = 1f;

    public float maxPitch = 1.0f;
    public float minPitch = 1.0f;

    public void PlaySound(AudioSource source, float sourceVolume)
    {
        source.volume = sourceVolume;
        source.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
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