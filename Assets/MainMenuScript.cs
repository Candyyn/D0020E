using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class MainMenuScript: MonoBehaviour
{
    public PinchSlider _pinchSlider;

    public PostProcessProfile brightness;

    public PostProcessLayer layer;

    private AutoExposure exposure;
    
    void Start()
    {
        brightness.TryGetSettings(out exposure);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public AudioMixer audioMixer;
    public void MethodSetVolume(SliderEventData eventData)
    {
        string strFloatValue = $"{eventData.NewValue:F2}";

        float floatValue = float.Parse(strFloatValue, CultureInfo.InvariantCulture.NumberFormat);
        
        
        audioMixer.SetFloat("volume", -1*floatValue);
        
        Debug.Log($"{-80*eventData.NewValue:F2}");
    }
    
    public void AdjustBrightness(SliderEventData eventData)
    {
        string strFloatValue = $"{eventData.NewValue:F2}";
        float floatValue = float.Parse(strFloatValue, CultureInfo.InvariantCulture.NumberFormat);

        if (floatValue != 0)
        {
            exposure.keyValue.value = floatValue/50;
        }

        else
        {
            exposure.keyValue.value = .05f;
        }
        
        SaveBrighntess(floatValue);
        
    }
    public void SaveBrighntess(float Data)
    {
        if (Data != 0)
        {
            exposure.keyValue.value = Data/50;
        }

        else
        {
            exposure.keyValue.value = .05f;
        }
        
    }

    
}