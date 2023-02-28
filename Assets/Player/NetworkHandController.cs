using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkHandController : MonoBehaviour
{
    [SerializeField] private GameObject[] ThumbJoints = new GameObject[3];
    [SerializeField] private GameObject[] IndexJoints = new GameObject[3];
    [SerializeField] private GameObject[] MiddleJoints = new GameObject[3];
    [SerializeField] private GameObject[] RingJoints = new GameObject[3];
    [SerializeField] private GameObject[] PinkyJoints = new GameObject[3];

    [SerializeField] private GameObject[] ProxyThumbJoints = new GameObject[3];
    [SerializeField] private GameObject[] ProxyIndexJoints = new GameObject[3];
    [SerializeField] private GameObject[] ProxyMiddleJoints = new GameObject[3];
    [SerializeField] private GameObject[] ProxyRingJoints = new GameObject[3];
    [SerializeField] private GameObject[] ProxyPinkyJoints = new GameObject[3];

    [SerializeField] private GameObject ProxyWrist;

    public string side = "Left";

    bool Inited = false;


    private void LateUpdate()
    {
        if (!Inited) return;

        SetChange(ProxyWrist.transform, gameObject.transform);
        
        foreach (var thumbJoint in ThumbJoints)
        {
            SetChange(ProxyThumbJoints[Array.IndexOf(ThumbJoints, thumbJoint)].transform, thumbJoint.transform);
        }

        foreach (var indexJoints in IndexJoints)
        {
            SetChange(ProxyIndexJoints[Array.IndexOf(IndexJoints, indexJoints)].transform, indexJoints.transform);
        }

        foreach (var indexJoints in MiddleJoints)
        {
            SetChange(ProxyMiddleJoints[Array.IndexOf(MiddleJoints, indexJoints)].transform, indexJoints.transform);
        }

        foreach (var indexJoints in RingJoints)
        {
            SetChange(ProxyRingJoints[Array.IndexOf(RingJoints, indexJoints)].transform, indexJoints.transform);
        }

        foreach (var indexJoints in PinkyJoints)
        {
            SetChange(ProxyPinkyJoints[Array.IndexOf(PinkyJoints, indexJoints)].transform, indexJoints.transform);
        }
    }

    private void SetChange(Transform Proxy, Transform Real)
    {
        Real.position = Proxy.position;
        Real.rotation = Proxy.rotation;
    }


    public void init()
    {
        ProxyWrist = gameObject.transform.parent.parent.parent.parent.parent.Find(side == "Left" ? "LNetworkHand" : "RNetworkHand").gameObject;

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Thumb"))
            {
                ThumbJoints[0] = child.gameObject;
                ThumbJoints[1] = child.GetChild(0).gameObject;
                ThumbJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Index"))
            {
                IndexJoints[0] = child.gameObject;
                IndexJoints[1] = child.GetChild(0).gameObject;
                IndexJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Middle"))
            {
                MiddleJoints[0] = child.gameObject;
                MiddleJoints[1] = child.GetChild(0).gameObject;
                MiddleJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Ring"))
            {
                RingJoints[0] = child.gameObject;
                RingJoints[1] = child.GetChild(0).gameObject;
                RingJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Pinky"))
            {
                PinkyJoints[0] = child.gameObject;
                PinkyJoints[1] = child.GetChild(0).gameObject;
                PinkyJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
        }

        foreach (Transform child in ProxyWrist.transform)
        {
            if (child.name.Contains("Thumb"))
            {
                ProxyThumbJoints[0] = child.gameObject;
                ProxyThumbJoints[1] = child.GetChild(0).gameObject;
                ProxyThumbJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Index"))
            {
                ProxyIndexJoints[0] = child.gameObject;
                ProxyIndexJoints[1] = child.GetChild(0).gameObject;
                ProxyIndexJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Middle"))
            {
                ProxyMiddleJoints[0] = child.gameObject;
                ProxyMiddleJoints[1] = child.GetChild(0).gameObject;
                ProxyMiddleJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Ring"))
            {
                ProxyRingJoints[0] = child.gameObject;
                ProxyRingJoints[1] = child.GetChild(0).gameObject;
                ProxyRingJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
            else if (child.name.Contains("Pinky"))
            {
                ProxyPinkyJoints[0] = child.gameObject;
                ProxyPinkyJoints[1] = child.GetChild(0).gameObject;
                ProxyPinkyJoints[2] = child.GetChild(0).GetChild(0).gameObject;
            }
        }

        Inited = true;
    }
}