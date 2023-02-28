using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Serialization;

public class handController : MonoBehaviour
{
    public string TagForHand = "RightHand";

    // Finger joints
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
    
    
    [SerializeField] public GameObject handProxy;
    
    
    bool Inited = false;

    // Update is called once per frame
    private void Update()
    {
        
        if(!Inited) return;
        
        MixedRealityPose pose;
        
        
        var handSide = TagForHand == "RightHand" ? Handedness.Right : Handedness.Left;

        //Set Wrist Position
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, handSide, out pose)) SetTransform(gameObject.transform, handProxy.transform, pose);

        // Set Index Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, handSide, out pose)) SetTransform(IndexJoints[0].transform, ProxyIndexJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexMiddleJoint, handSide, out pose)) SetTransform(IndexJoints[1].transform, ProxyIndexJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, handSide, out pose)) SetTransform(IndexJoints[2].transform, ProxyIndexJoints[2].transform, pose);


        // Set Middle Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleKnuckle, handSide, out pose)) SetTransform(MiddleJoints[0].transform, ProxyMiddleJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleMiddleJoint, handSide, out pose)) SetTransform(MiddleJoints[1].transform, ProxyMiddleJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, handSide, out pose)) SetTransform(MiddleJoints[2].transform, ProxyMiddleJoints[2].transform, pose);

        // Set Thumb
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbProximalJoint, handSide, out pose)) SetTransform(ThumbJoints[0].transform, ProxyThumbJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbDistalJoint, handSide, out pose)) SetTransform(ThumbJoints[1].transform, ProxyThumbJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, handSide, out pose)) SetTransform(ThumbJoints[2].transform, ProxyThumbJoints[2].transform, pose);


        // Set Ring Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingKnuckle, handSide, out pose)) SetTransform(RingJoints[0].transform, ProxyRingJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingMiddleJoint, handSide, out pose)) SetTransform(RingJoints[1].transform, ProxyRingJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, handSide, out pose)) SetTransform(RingJoints[2].transform, ProxyRingJoints[2].transform, pose);

        // Set Pinky Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, handSide, out pose)) SetTransform(PinkyJoints[0].transform, ProxyPinkyJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyMiddleJoint, handSide, out pose)) SetTransform(PinkyJoints[1].transform, ProxyPinkyJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, handSide, out pose)) SetTransform(PinkyJoints[2].transform, ProxyPinkyJoints[2].transform, pose);
    }


    void SetTransform(Transform transform, Transform Proxy, MixedRealityPose pose)
    {
        transform.position = pose.Position;
        Proxy.position = pose.Position;
        transform.rotation = pose.Rotation * Quaternion.Euler(90, 0, 0);
        Proxy.rotation = pose.Rotation * Quaternion.Euler(90, 0, 0);
    }

    public void Init()
    {
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
        
        foreach (Transform child in handProxy.transform)
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