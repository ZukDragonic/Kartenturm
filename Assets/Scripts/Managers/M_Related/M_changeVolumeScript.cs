using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_changeVolumeScript : MonoBehaviour
{
    public Slider slider;
    public SoundType type;

    void Start()
    {
        gameControllerScript gc = gameControllerScript.instance;
        switch (type)
        {
            case SoundType.Master:
                slider.value = gc.audioManager.masterVolume;
                break;
            case SoundType.SFX:
                slider.value = gc.audioManager.SFXVolume;
                break;
            case SoundType.Music:
                slider.value = gc.audioManager.musicVolume;
                break;
            default:
                break;
        }
    }
    void Update()
    {
        UpdateVolume(slider.value);
    }
    
    public void UpdateVolume(float volume)
    {
        gameControllerScript gc = gameControllerScript.instance;
        switch (type)
        {
            case SoundType.Master:
                gc.audioManager.masterVolume = volume;
                break;
            case SoundType.SFX:
                gc.audioManager.SFXVolume = volume;
                break;
            case SoundType.Music:
                gc.audioManager.musicVolume = volume;
                break;
            default:
                break;
        }
    }
}
