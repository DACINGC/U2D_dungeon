using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string paramter;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mutiplier;

    public void SliderValume(float _value)
    {
        audioMixer.SetFloat(paramter, Mathf.Log10(_value) * mutiplier);
    }

    public void LoadeSliderValue(float _value)
    {
        if(_value != 0)
            slider.value = _value;
    }
}
