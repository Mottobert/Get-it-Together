using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    private int firstPlay;
    public Slider musikSlider, soundeffectsSlider; 
    private float musikFloat, soundeffectsFloat;

    [SerializeField]
    private TextMeshProUGUI musikPercentageLabel;
    [SerializeField]
    private TextMeshProUGUI sfxPercentageLabel;

    private float musikFloatReset, soundeffectsFloatReset;

    public void SetLevelMusic(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("MusicVolume", -80);
            //musikPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
            //PlayerPrefs.SetFloat("MusikFloat", sliderValue);
        }
        else
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        }

        musikPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
        PlayerPrefs.SetFloat("MusikFloat", sliderValue);
    }

    public void SetLevelSoundeffect(float sliderValue)
    {
        if(sliderValue == 0)
        {
            mixer.SetFloat("SfxVolume", -80);
            //sfxPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
            //PlayerPrefs.SetFloat("SoundEffectsFloat", sliderValue);
        }
        else
        {
            mixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20);
        }

        sfxPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
        PlayerPrefs.SetFloat("SoundEffectsFloat", sliderValue);
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
            SetLevelMusic(musikFloat);
            soundeffectsFloat = PlayerPrefs.GetFloat("SoundEffectsFloat");
            soundeffectsSlider.value = soundeffectsFloat;
            SetLevelSoundeffect(soundeffectsFloat);
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
        if(musikSlider.value > 0)
        {
            musikFloatReset = musikSlider.value;
            musikFloat = 0f;
            musikSlider.value = musikFloat;
            SetLevelMusic(musikFloat);
            
        }
        else if(musikSlider.value == 0)
        {
            musikFloat = musikFloatReset;
            musikSlider.value = musikFloat;
            SetLevelMusic(musikFloat);
        }
    }

    public void ToggleMuteSfx()
    {
        if (soundeffectsSlider.value > 0)
        {
            soundeffectsFloatReset = soundeffectsSlider.value;
            soundeffectsFloat = 0f;
            soundeffectsSlider.value = soundeffectsFloat;
            SetLevelSoundeffect(soundeffectsFloat);
        }
        else if (soundeffectsSlider.value == 0)
        {
            soundeffectsFloat = soundeffectsFloatReset;
            soundeffectsSlider.value = soundeffectsFloat;
            SetLevelSoundeffect(soundeffectsFloat);
        }
    }
}
