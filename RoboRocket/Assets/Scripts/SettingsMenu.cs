using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    GameObject slider;

    void Start()
    {
        slider = GameObject.Find("VolumeEditor");
        slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool IsFull)
    {
        Screen.fullScreen = IsFull;
    }
}
