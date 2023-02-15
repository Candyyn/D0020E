using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MainMenuScript: MonoBehaviour
{

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

    
}