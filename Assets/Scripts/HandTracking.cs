using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class HandTracking : MonoBehaviour
{
    public GameObject redMarker;
    public GameObject orangeMarker;
    public GameObject yellowMarker;
    public GameObject greenMarker;
    public GameObject blueMarker;
    public GameObject wristMarker;
    
    GameObject thumbObjectR;
    GameObject indexObjectR;
    GameObject middleObjectR;
    GameObject ringObjectR;
    GameObject pinkyObjectR;
    GameObject wristObjectR;
    
    GameObject thumbObjectL;
    GameObject indexObjectL;
    GameObject middleObjectL;
    GameObject ringObjectL;
    GameObject pinkyObjectL;
    GameObject wristObjectL;
    
    private MixedRealityPose pose;
    void Start()
    {
        thumbObjectR = Instantiate(blueMarker, this.transform);
        indexObjectR = Instantiate(greenMarker, this.transform);
        middleObjectR = Instantiate(yellowMarker, this.transform);
        ringObjectR = Instantiate(orangeMarker, this.transform);
        pinkyObjectR = Instantiate(redMarker, this.transform);
        wristObjectR = Instantiate(wristMarker, this.transform);

        thumbObjectL = Instantiate(blueMarker, this.transform);
        indexObjectL = Instantiate(greenMarker, this.transform);
        middleObjectL = Instantiate(yellowMarker, this.transform);
        ringObjectL = Instantiate(orangeMarker, this.transform);
        pinkyObjectL = Instantiate(redMarker, this.transform);
        wristObjectL = Instantiate(wristMarker, this.transform);
        
        //handObject = Instantiate(handMarker, this.transform);
    }

    void Update()
    {
        thumbObjectR.GetComponent<Renderer>().enabled = false;
        indexObjectR.GetComponent<Renderer>().enabled = false;
        middleObjectR.GetComponent<Renderer>().enabled = false;
        ringObjectR.GetComponent<Renderer>().enabled = false;
        pinkyObjectR.GetComponent<Renderer>().enabled = false;
        wristObjectR.GetComponent<Renderer>().enabled = false;
        
        thumbObjectL.GetComponent<Renderer>().enabled = false;
        indexObjectL.GetComponent<Renderer>().enabled = false;
        middleObjectL.GetComponent<Renderer>().enabled = false;
        ringObjectL.GetComponent<Renderer>().enabled = false;
        pinkyObjectL.GetComponent<Renderer>().enabled = false;
        wristObjectL.GetComponent<Renderer>().enabled = false;

        //handObject.GetComponent<Renderer>().enabled = false;
        
        // RIGHT HAND

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
        {
            thumbObjectR.GetComponent<Renderer>().enabled = true;
            thumbObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
        {
            indexObjectR.GetComponent<Renderer>().enabled = true;
            indexObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
        {
            middleObjectR.GetComponent<Renderer>().enabled = true;
            middleObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
        {
            ringObjectR.GetComponent<Renderer>().enabled = true;
            ringObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
        {
            pinkyObjectR.GetComponent<Renderer>().enabled = true;
            pinkyObjectR.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Right, out pose))
        {
            wristObjectR.GetComponent<Renderer>().enabled = true;
            wristObjectR.transform.position = pose.Position;
        }       
        //if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out pose))
        //{
        //    handObject.GetComponent<Renderer>().enabled = true;
        //    handObject.transform.position = pose.Position;
        //}
        
        
        // LEFT HAND
        
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose))
        {
            thumbObjectL.GetComponent<Renderer>().enabled = true;
            thumbObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))
        {
            indexObjectL.GetComponent<Renderer>().enabled = true;
            indexObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out pose))
        {
            middleObjectL.GetComponent<Renderer>().enabled = true;
            middleObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out pose))
        {
            ringObjectL.GetComponent<Renderer>().enabled = true;
            ringObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out pose))
        {
            pinkyObjectL.GetComponent<Renderer>().enabled = true;
            pinkyObjectL.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left, out pose))
        {
            wristObjectL.GetComponent<Renderer>().enabled = true;
            wristObjectL.transform.position = pose.Position;
        } 
    }
}
