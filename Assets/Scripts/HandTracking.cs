using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Unity.Netcode;

public class HandTracking : NetworkBehaviour
{
    public GameObject redMarker;
    public GameObject orangeMarker;
    public GameObject yellowMarker;
    public GameObject greenMarker;
    public GameObject blueMarker;
    public GameObject wristMarker;

    GameObject thumbObjectR;
    GameObject thumbObjectR1;
    GameObject thumbObjectR2;
    
    GameObject indexObjectR;
    GameObject indexObjectR1;
    GameObject indexObjectR2;
    
    GameObject middleObjectR;
    GameObject middleObjectR1;
    GameObject middleObjectR2;
    
    GameObject ringObjectR;
    GameObject ringObjectR1;
    GameObject ringObjectR2;
    
    GameObject pinkyObjectR;
    GameObject pinkyObjectR1;
    GameObject pinkyObjectR2;
    
    GameObject wristObjectR;

    GameObject thumbObjectL;
    GameObject indexObjectL;
    GameObject middleObjectL;
    GameObject ringObjectL;
    GameObject pinkyObjectL;
    GameObject wristObjectL;

    private MixedRealityPose pose;

    private void Start()
    {
        
        wristObjectR = InstantiateObject(wristMarker, OwnerClientId, this.transform);
        wristObjectR.name = "wristObjectR";
        
        
        thumbObjectR2 = InstantiateObject(blueMarker, OwnerClientId, wristObjectR.transform);
        thumbObjectR1 = InstantiateObject(blueMarker, OwnerClientId, thumbObjectR2.transform);
        thumbObjectR = InstantiateObject(blueMarker, OwnerClientId, thumbObjectR1.transform);

        thumbObjectR.name = "thumbObjectR";
        thumbObjectR1.name = "thumbObjectR1";
        thumbObjectR2.name = "thumbObjectR2";
        
        
        indexObjectR = InstantiateObject(blueMarker, OwnerClientId, wristObjectR.transform);
        indexObjectR.name = "indexObjectR";
        indexObjectR1 = InstantiateObject(blueMarker, OwnerClientId, indexObjectR.transform);
        indexObjectR1.name = "indexObjectR1";
        indexObjectR2 = InstantiateObject(blueMarker, OwnerClientId, indexObjectR1.transform);
        indexObjectR2.name = "indexObjectR2";

        
        //indexObjectR = InstantiateObject(greenMarker, OwnerClientId, this.transform);
        middleObjectR2 = InstantiateObject(yellowMarker, OwnerClientId, wristObjectR.transform);
        middleObjectR1 = InstantiateObject(yellowMarker, OwnerClientId, middleObjectR2.transform);
        middleObjectR = InstantiateObject(yellowMarker, OwnerClientId, middleObjectR1.transform);

        middleObjectR2.name = "middleObjectR2";
        middleObjectR1.name = "middleObjectR1";
        middleObjectR.name = "middleObjectR";

        
        ringObjectR = InstantiateObject(orangeMarker, OwnerClientId, wristObjectR.transform);
        ringObjectR.name = "ringObjectR";
        ringObjectR1 = InstantiateObject(orangeMarker, OwnerClientId, ringObjectR.transform);
        ringObjectR1.name = "ringObjectR1";
        ringObjectR2 = InstantiateObject(orangeMarker, OwnerClientId, ringObjectR1.transform);
        ringObjectR2.name = "ringObjectR2";
        
        
        pinkyObjectR = InstantiateObject(redMarker, OwnerClientId, wristObjectR.transform);
        pinkyObjectR.name = "pinkyObjectR";
        pinkyObjectR1 = InstantiateObject(redMarker, OwnerClientId, pinkyObjectR.transform);
        pinkyObjectR1.name = "pinkyObjectR1";
        pinkyObjectR2 = InstantiateObject(redMarker, OwnerClientId, pinkyObjectR1.transform);
        pinkyObjectR2.name = "pinkyObjectR2";
        
        
        


        thumbObjectL = InstantiateObject(blueMarker, OwnerClientId, this.transform);
        indexObjectL = InstantiateObject(greenMarker, OwnerClientId, this.transform);
        middleObjectL = InstantiateObject(yellowMarker, OwnerClientId, this.transform);
        ringObjectL = InstantiateObject(orangeMarker, OwnerClientId, this.transform);
        pinkyObjectL = InstantiateObject(redMarker, OwnerClientId, this.transform);
        wristObjectL = InstantiateObject(wristMarker, OwnerClientId, this.transform);

        
        
        thumbObjectR.tag = "RightHand";
        thumbObjectR1.tag = "RightHand";
        thumbObjectR2.tag = "RightHand";
        
        indexObjectR.tag = "RightHand";
        indexObjectR1.tag = "RightHand";
        indexObjectR2.tag = "RightHand";
        
        
        middleObjectR.tag = "RightHand";
        middleObjectR1.tag = "RightHand";
        middleObjectR2.tag = "RightHand";
        
        ringObjectR.tag = "RightHand";
        ringObjectR1.tag = "RightHand";
        ringObjectR2.tag = "RightHand";
        
        pinkyObjectR.tag = "RightHand";
        pinkyObjectR1.tag = "RightHand";
        pinkyObjectR2.tag = "RightHand";
        
        wristObjectR.tag = "RightHand";

        //set tag on objects
        thumbObjectL.tag = "LeftHand";
        indexObjectL.tag = "LeftHand";
        middleObjectL.tag = "LeftHand";
        ringObjectL.tag = "LeftHand";
        pinkyObjectL.tag = "LeftHand";
        wristObjectL.tag = "LeftHand";


        /*thumbObjectR.GetComponent<Renderer>().enabled = false;
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
        wristObjectL.GetComponent<Renderer>().enabled = false;*/

        //set tag on objects


        //handObject = Instantiate(handMarker, this.transform);
    }

    void Update()
    {
        try
        {
        }
        catch (System.Exception e)
        {
            return;
        }

        if (!IsOwner) return;


        //handObject.GetComponent<Renderer>().enabled = false;

        // RIGHT HAND

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
        {
            //thumbObjectR.GetComponent<Renderer>().enabled = true;
            thumbObjectR.transform.position = pose.Position;
            thumbObjectR.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbDistalJoint, Handedness.Right, out pose))
        {
            //thumbObjectR.GetComponent<Renderer>().enabled = true;
            thumbObjectR1.transform.position = pose.Position;
            thumbObjectR1.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbProximalJoint, Handedness.Right, out pose))
        {
            //thumbObjectR.GetComponent<Renderer>().enabled = true;
            thumbObjectR2.transform.position = pose.Position;
            thumbObjectR2.transform.rotation = pose.Rotation;
        }
        


        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
        {
            //indexObjectR.GetComponent<Renderer>().enabled = true;
            indexObjectR.transform.position = pose.Position;
            indexObjectR.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexMiddleJoint, Handedness.Right, out pose))
        {
            //indexObjectR.GetComponent<Renderer>().enabled = true;
            indexObjectR1.transform.position = pose.Position;
            indexObjectR1.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Right, out pose))
        {
            //indexObjectR.GetComponent<Renderer>().enabled = true;
            indexObjectR2.transform.position = pose.Position;
            indexObjectR2.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
        {
            //middleObjectR.GetComponent<Renderer>().enabled = true;
            middleObjectR.transform.position = pose.Position;
            middleObjectR.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleMiddleJoint, Handedness.Right, out pose))
        {
            //middleObjectR.GetComponent<Renderer>().enabled = true;
            middleObjectR1.transform.position = pose.Position;
            middleObjectR1.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleKnuckle, Handedness.Right, out pose))
        {
            //middleObjectR.GetComponent<Renderer>().enabled = true;
            middleObjectR2.transform.position = pose.Position;
            middleObjectR2.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
        {
            //ringObjectR.GetComponent<Renderer>().enabled = true;
            ringObjectR.transform.position = pose.Position;
            ringObjectR.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingMiddleJoint, Handedness.Right, out pose))
        {
            //ringObjectR.GetComponent<Renderer>().enabled = true;
            ringObjectR1.transform.position = pose.Position;
            ringObjectR1.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingKnuckle, Handedness.Right, out pose))
        {
            //ringObjectR.GetComponent<Renderer>().enabled = true;
            ringObjectR2.transform.position = pose.Position;
            ringObjectR2.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
        {
            //pinkyObjectR.GetComponent<Renderer>().enabled = true;
            pinkyObjectR.transform.position = pose.Position;
            pinkyObjectR.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyMiddleJoint, Handedness.Right, out pose))
        {
            //pinkyObjectR.GetComponent<Renderer>().enabled = true;
            pinkyObjectR1.transform.position = pose.Position;
            pinkyObjectR1.transform.rotation = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, Handedness.Right, out pose))
        {
            //pinkyObjectR.GetComponent<Renderer>().enabled = true;
            pinkyObjectR2.transform.position = pose.Position;
            pinkyObjectR2.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Right, out pose))
        {
            //wristObjectR.GetComponent<Renderer>().enabled = true;
            wristObjectR.transform.position = pose.Position;
            wristObjectR.transform.rotation = pose.Rotation;
        }
        //if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out pose))
        //{
        //    handObject.GetComponent<Renderer>().enabled = true;
        //    handObject.transform.position = pose.Position;
        //}


        // LEFT HAND

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose))
        {
            //thumbObjectL.GetComponent<Renderer>().enabled = true;
            thumbObjectL.transform.position = pose.Position;
            thumbObjectL.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))
        {
            //indexObjectL.GetComponent<Renderer>().enabled = true;
            indexObjectL.transform.position = pose.Position;
            indexObjectL.transform.rotation = pose.Rotation;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out pose))
        {
            //middleObjectL.GetComponent<Renderer>().enabled = true;
            middleObjectL.transform.position = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out pose))
        {
            //ringObjectL.GetComponent<Renderer>().enabled = true;
            ringObjectL.transform.position = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out pose))
        {
            //pinkyObjectL.GetComponent<Renderer>().enabled = true;
            pinkyObjectL.transform.position = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left, out pose))
        {
            //wristObjectL.GetComponent<Renderer>().enabled = true;
            wristObjectL.transform.position = pose.Position;
        }
    }


    private GameObject InstantiateObject(GameObject obj, ulong owner, Transform parent = null)
    {
        var instance = Instantiate(obj);
        instance.GetComponent<NetworkObject>().SpawnWithOwnership(owner);
        //instance.transform.localPosition = new Vector3(0, 0, 0);
        if (parent != null)
        {
            instance.transform.SetParent(parent);
        }

        return instance;
    }
}