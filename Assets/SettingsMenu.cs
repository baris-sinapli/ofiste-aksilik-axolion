using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private GameObject volumeButton;
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private Sprite[] volumeSprites;
    private bool on = true;
    private float lastVolume = 0f;

    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void ToggleVolume()
    {
        if (on)
        {
            volumeButton.GetComponent<Image>().sprite = volumeSprites[0];
            lastVolume = SoundSlider.value;
            SoundSlider.value = SoundSlider.minValue;
            on = false;
        }
        else
        {
            volumeButton.GetComponent<Image>().sprite = volumeSprites[1];
            SoundSlider.value = lastVolume;
            on = true;
        }
        
    }
}
