using System;

using UnityEngine;
using UnityEngine.Events;


[Serializable] public class FloatEvent : UnityEvent<float> { }
public class SetVolume : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
