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

    /*[HideInInspector]
    public List<AudioSource> SFXSource;
    [HideInInspector]
    public List<AudioSource> MusicSource;*/
    //no clue why I made this but must be here for a reason

    private bool isInitialized = false;

    void Start()
    {
        InitalizeAudioSource();
    }
    public void PlaySound(int ID)
    {
        Sound current = Array.Find(sounds, sound => sound.ID == ID);
        if (current == null)
        {
            Debug.Log("Fuck off and enter an an actual audio ID.");
            return;
        }

        //checks for any empty instances it can be played on and if it doesn't exist it overrides the earliest instance
        AudioSource source = CheckEmptySource(current.source);
        if (source == null)
        {
            source = CheckFirstSource(current.source);
        }

        //randomizes the clip played if possible
        AudioClip playedClip = current.clips[0];
        if (current.clips.Length > 1)
        {
            playedClip = current.clips[UnityEngine.Random.Range(0, current.clips.Length)];
        }

        source.clip = playedClip;

        //sets volume
        if (current.type == SoundType.SFX)
        {
            source.volume = current.volume * masterVolume * SFXVolume;
        }
        else
        {
            source.volume = current.volume * masterVolume * musicVolume;
        }

        source.Play();
    }

    AudioSource CheckEmptySource(AudioSource[] sourceList)
    {
        foreach (AudioSource source in sourceList)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    AudioSource CheckFirstSource(AudioSource[] sourceList)
    {
        AudioSource current = sourceList[0];
        foreach (AudioSource source in sourceList)
        {
            if (source.time > current.time)
            {
                current = source;
            }
        }
        return current;
    }

    void InitalizeAudioSource()
    {
        if (isInitialized)
        {
            return;
        }
        foreach (Sound sound in sounds)
        {
            List<AudioSource> sourceList = new List<AudioSource>();
            for (int i = 0; i < sound.maxInstances; i++)
            {
                AudioSource current = gameObject.AddComponent<AudioSource>();
                sourceList.Add(current);
                current.clip = sound.clips[0];
                current.pitch = sound.pitch;
                /*if (sound.type == SoundType.SFX)
                {
                    SFXSource.Add(current);
                }
                else
                {
                    MusicSource.Add(current);
                }*/
            }
            sound.source = sourceList.ToArray();
        }
    }
}