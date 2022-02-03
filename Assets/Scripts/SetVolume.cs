using MoreMountains.Feedbacks;
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
    public Slider musikSlider, soundeffectsSlider, uiSlider; 
    private float musikFloat, soundeffectsFloat, uiFloat;

    [SerializeField]
    private TextMeshProUGUI musikPercentageLabel;
    [SerializeField]
    private TextMeshProUGUI sfxPercentageLabel;
    [SerializeField]
    private TextMeshProUGUI uiPercentageLabel;

    [SerializeField]
    private GameObject muteCrossMusic;
    [SerializeField]
    private GameObject muteCrossSfx;
    [SerializeField]
    private GameObject muteCrossUI;

    private float musikFloatReset, soundeffectsFloatReset, uiFloatReset;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");
        firstPlay = PlayerPrefs.GetInt("FirstPlay");

        if (firstPlay == 0)
        {
            musikFloat = 0.5f;
            soundeffectsFloat = 0.75f;
            uiFloat = 0.75f;
            musikSlider.value = musikFloat;
            soundeffectsSlider.value = soundeffectsFloat;
            uiSlider.value = uiFloat;
            PlayerPrefs.SetFloat("MusikFloat", musikFloat);
            PlayerPrefs.SetFloat("SoundEffectsFloat", soundeffectsFloat);
            PlayerPrefs.SetFloat("uiFloat", uiFloat);
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
            uiFloat = PlayerPrefs.GetFloat("uiFloat");
            uiSlider.value = uiFloat;
            SetLevelUI(uiFloat);
        }
    }
    public void SetLevelMusic(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("MusicVolume", -80);
            muteCrossMusic.SetActive(true);
            //musikPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
            //PlayerPrefs.SetFloat("MusikFloat", sliderValue);
        }
        else
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
            muteCrossMusic.SetActive(false);
        }

        musikPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
        PlayerPrefs.SetFloat("MusikFloat", sliderValue);
    }

    public void SetLevelSoundeffect(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("SfxVolume", -80);
            muteCrossSfx.SetActive(true);
            //sfxPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
            //PlayerPrefs.SetFloat("SoundEffectsFloat", sliderValue);
        }
        else
        {
            mixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20);
            muteCrossSfx.SetActive(false);
        }

        sfxPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
        PlayerPrefs.SetFloat("SoundEffectsFloat", sliderValue);
    }

    public void SetLevelUI(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("UiVolume", -80);
            muteCrossUI.SetActive(true);
            //sfxPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
            //PlayerPrefs.SetFloat("SoundEffectsFloat", sliderValue);
        }
        else
        {
            mixer.SetFloat("UiVolume", Mathf.Log10(sliderValue) * 20);
            muteCrossUI.SetActive(false);
        }

        uiPercentageLabel.text = "" + Mathf.RoundToInt(sliderValue * 100);
        PlayerPrefs.SetFloat("uiFloat", sliderValue);
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MusikFloat", musikSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsFloat", soundeffectsSlider.value);
        PlayerPrefs.SetFloat("uiFloat", uiSlider.value);
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
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (musikSlider.value > 0)
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
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
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

    public void ToggleMuteUI()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (uiSlider.value > 0)
        {
            uiFloatReset = uiSlider.value;
            uiFloat = 0f;
            uiSlider.value = uiFloat;
            SetLevelUI(uiFloat);
        }
        else if (uiSlider.value == 0)
        {
            uiFloat = uiFloatReset;
            uiSlider.value = uiFloat;
            SetLevelUI(uiFloat);
        }
    }
}
