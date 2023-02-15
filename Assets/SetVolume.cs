using System;

using UnityEngine;
using UnityEngine.Events;


[Serializable] public class FloatEvent : UnityEvent<float> { }
public class SetVolume : MonoBehaviour
{
    


    public void MethSetVol(float volume)
    {
        Debug.Log(volume);
    }
}
