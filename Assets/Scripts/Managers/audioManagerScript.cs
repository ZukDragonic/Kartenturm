using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    public float masterVolume;
    public float SFXVolume;
    public float musicVolume;

    public List<AudioSource> SFXSource;
    public List<AudioSource> MusicSource;

    void Start()
    {
        foreach (Sound sound in sounds)
        {
            AudioSource current = gameObject.AddComponent<AudioSource>();
            sound.source = current;
            current.clip = sound.clips[0];
            sound.source.pitch = sound.pitch;
            if (sound.type == SoundType.SFX)
            {
                SFXSource.Add(sound.source);
            }
            else
            {
                MusicSource.Add(sound.source);
            }
        }
    }
    public void PlaySound(int ID)
    {
        Sound current = Array.Find(sounds, sound => sound.ID == ID);
        if (current == null)
        {
            Debug.Log("Fuck off and enter an an actual audio ID.");
            return;
        }
        AudioClip playedClip = current.clips[0];
        if (current.clips.Length > 1)
        {
            playedClip = current.clips[UnityEngine.Random.Range(0, current.clips.Length)];
        }
        current.source.clip = playedClip;
        if (current.type == SoundType.SFX)
        {
            current.source.volume = current.volume * masterVolume * SFXVolume;
        }
        else
        {
            current.source.volume = current.volume * masterVolume * musicVolume;
        }
        current.source.Play();
    }
}