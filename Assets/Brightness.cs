using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class Brightness : MonoBehaviour
{
    public PinchSlider _pinchSlider;

    public PostProcessProfile brightness;

    public PostProcessLayer layer;

    private AutoExposure exposure;

    public Slider _brightslider;
    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
        exposure.keyValue.value = PlayerPrefs.GetFloat("Brightness");
        _brightslider.value = PlayerPrefs.GetFloat("Brightness");
        
    }

    public void AdjustBrightness(SliderEventData eventData)
    {
        string strFloatValue = $"{eventData.NewValue:F2}";
        float floatValue = float.Parse(strFloatValue, CultureInfo.InvariantCulture.NumberFormat);

        if (floatValue != 0)
        {
            exposure.keyValue.value = floatValue/50;
            PlayerPrefs.SetFloat("Brightness", exposure.keyValue.value);
        }

        else
        {
            exposure.keyValue.value = .05f;
            PlayerPrefs.SetFloat("Brightness", exposure.keyValue.value);
        }
        
     
        
    }

    public void UpdateBrighntess(float Brightness)
    {
        exposure.keyValue.value = Brightness;

    }
    
}
