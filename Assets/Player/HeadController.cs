using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HeadController : MonoBehaviour
{
    public GameObject Head;
    public GameObject RightEye;
    public GameObject LeftEye;


    //TODO: Implement eye and Head tracking
    //TODO: Also implment a Network Proxy for the Head and Eye Tracking

    private void Start()
    {
        Head = transform.GetChild(0).gameObject;

        // Loop over Head children
        foreach (Transform child in Head.transform)
        {
            switch (child.name)
            {
                // If child name is LeftEye
                case "LeftEye":
                    // Set LeftEye to child
                    LeftEye = child.gameObject;
                    break;
                // If child name is RightEye
                case "RightEye":
                    // Set RightEye to child
                    RightEye = child.gameObject;
                    break;
            }
        }
    }
}