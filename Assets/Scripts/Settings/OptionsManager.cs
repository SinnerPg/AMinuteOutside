using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Text volumeText;
    public Slider volumeSlider;
    public GameObject canvas;
    public static float volume;
    void Awake()
    {
        volume = PlayerPrefs.GetFloat("volume", 50);
    }
    void Start()
    {
        volumeSlider.value = volume * 0.01f;
    }
    void Update()
    {
        volumeText.text = Mathf.Round(volumeSlider.value * 100).ToString();
        volume = Mathf.Round(volumeSlider.value * 100);
    }

    public void openCanvas()
    {
        canvas.SetActive(true);
    }
    public void saveOptions()
    {
        PlayerPrefs.SetFloat("volume", volume);
        canvas.SetActive(false);
    }
}
