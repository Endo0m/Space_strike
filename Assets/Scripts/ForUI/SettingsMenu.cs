using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private string volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer audioMixer;
    private const float _multiplier = 20;

    private void Awake()
    {
        soundSlider.onValueChanged.AddListener(HandleSliderValueChanged);
        LoadSettings();
    }

    private void HandleSliderValueChanged(float value)
    {
        var volumeValue = Mathf.Log10(value) * _multiplier;
        audioMixer.SetFloat(volumeParameter, volumeValue);
        PlayerPrefs.SetFloat(volumeParameter, value);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(volumeParameter))
        {
            float savedValue = PlayerPrefs.GetFloat(volumeParameter);
            soundSlider.value = savedValue;
            audioMixer.SetFloat(volumeParameter, Mathf.Log10(savedValue) * _multiplier);
        }
    }
}