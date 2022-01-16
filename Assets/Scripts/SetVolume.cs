using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    private int firstPlay;
    public Slider musikSlider, soundeffectsSlider; 
    private float musikFloat, soundeffectsFloat;

    private float musikFloatReset, soundeffectsFloatReset;

    public void SetLevelMusic(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        }
    }

    public void SetLevelSoundeffect(float sliderValue)
    {
        if(sliderValue == 0)
        {
            mixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20);
        }
        else
        {
            mixer.SetFloat("SfxVolume", -80);
        }
    }

    private void Start()
    {
        firstPlay = PlayerPrefs.GetInt("FirstPlay");

        if (firstPlay == 0)
        {
            musikFloat = 0.5f;
            soundeffectsFloat = 0.75f;
            musikSlider.value = musikFloat;
            soundeffectsSlider.value = soundeffectsFloat;
            PlayerPrefs.SetFloat("MusikFloat", musikFloat);
            PlayerPrefs.SetFloat("SoundEffectsFloat", soundeffectsFloat);
            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
            musikFloat = PlayerPrefs.GetFloat("MusikFloat");
            musikSlider.value = musikFloat;
            soundeffectsFloat = PlayerPrefs.GetFloat("SoundEffectsFloat");
            soundeffectsSlider.value = soundeffectsFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MusikFloat", musikSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsFloat", soundeffectsSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettings();
        }
    }

    public void ToggleMuteMusic()
    {
        if(musikFloat > 0)
        {
            musikFloatReset = musikSlider.value;
            musikFloat = 0f;
            musikSlider.value = musikFloat;
            SetLevelMusic(musikFloat);
        }
        else if(musikFloat == 0)
        {
            musikFloat = musikFloatReset;
            musikSlider.value = musikFloat;
            SetLevelMusic(musikFloat);
        }
    }

    public void ToggleMuteSfx()
    {
        if (soundeffectsFloat > 0)
        {
            soundeffectsFloatReset = soundeffectsSlider.value;
            soundeffectsFloat = 0f;
            soundeffectsSlider.value = soundeffectsFloat;
            SetLevelSoundeffect(soundeffectsFloat);
        }
        else if (soundeffectsFloat == 0)
        {
            soundeffectsFloat = soundeffectsFloatReset;
            soundeffectsSlider.value = soundeffectsFloat;
            SetLevelSoundeffect(soundeffectsFloat);
        }
    }
}
