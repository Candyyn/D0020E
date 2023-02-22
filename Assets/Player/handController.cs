using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class handController : MonoBehaviour
{
    public string TagForHand = "RightHand";

    // Create an array of GameObjects to hold hands


    // What is being tracked
    [SerializeField] private GameObject[] thumbObject = new GameObject[3];
    [SerializeField] GameObject[] indexObject = new GameObject[3];

    [SerializeField] GameObject[] middleObject = new GameObject[3];
    [SerializeField] GameObject[] ringObject = new GameObject[3];
    [SerializeField] GameObject[] pinkyObject = new GameObject[3];
    [SerializeField] GameObject wristObject;

    // Finger joints
    [SerializeField] private GameObject[] ThumbJoints = new GameObject[3];
    [SerializeField] private GameObject[] IndexJoints = new GameObject[3];
    [SerializeField] private GameObject[] MiddleJoints = new GameObject[3];
    [SerializeField] private GameObject[] RingJoints = new GameObject[3];
    [SerializeField] private GameObject[] PinkyJoints = new GameObject[3];


    enum Finger
    {
        Thumb,
        Index,
        Middle,
        Ring,
        Pinky
    }


    // Start is called before the first frame update
    private void Start()
    {
        Init();

        //Go over child objects and check their name contains the name of the finger
    }


    int initCount = 0;

    // Update is called once per frame
    private void Update()
    {
        MixedRealityPose pose;
        
        
        Handedness handSide = TagForHand == "RightHand" ? Handedness.Right : Handedness.Left;

        //Set Wrist Position
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, handSide, out pose)) SetTransform(gameObject.transform, pose);

        // Set Index Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, handSide, out pose)) SetTransform(IndexJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexMiddleJoint, handSide, out pose)) SetTransform(IndexJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, handSide, out pose)) SetTransform(IndexJoints[2].transform, pose);


        // Set Middle Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleKnuckle, handSide, out pose)) SetTransform(MiddleJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleMiddleJoint, handSide, out pose)) SetTransform(MiddleJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, handSide, out pose)) SetTransform(MiddleJoints[2].transform, pose);

        // Set Thumb
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbProximalJoint, handSide, out pose)) SetTransform(ThumbJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbDistalJoint, handSide, out pose)) SetTransform(ThumbJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, handSide, out pose)) SetTransform(ThumbJoints[2].transform, pose);


        // Set Ring Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingKnuckle, handSide, out pose)) SetTransform(RingJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingMiddleJoint, handSide, out pose)) SetTransform(RingJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, handSide, out pose)) SetTransform(RingJoints[2].transform, pose);

        // Set Pinky Finger
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, handSide, out pose)) SetTransform(PinkyJoints[0].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyMiddleJoint, handSide, out pose)) SetTransform(PinkyJoints[1].transform, pose);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, handSide, out pose)) SetTransform(PinkyJoints[2].transform, pose);

        return;
    }


    void SetTransform(Transform transform, MixedRealityPose pose)
    {
        transform.position = pose.Position;
        transform.rotation = pose.Rotation * Quaternion.Euler(90, 0, 0);
    }

    private void Init()
    {
        Debug.Log("Init");
        //Find all gameobjects with the tag "RightHand"
        GameObject[] rightHand = GameObject.FindGameObjectsWithTag(TagForHand);

        // Go through all and check its name
        foreach (GameObject hand in rightHand)
        {
            if (hand.name == "thumbObjectR")
            {
                thumbObject[0] = hand;
            }
            else if (hand.name == "thumbObjectR1")
            {
                thumbObject[1] = hand;
            }
            else if (hand.name == "thumbObjectR2")
            {
                thumbObject[2] = hand;
            }
            else if (hand.name == "indexObjectR")
            {
                indexObject[0] = hand;
            }
            else if (hand.name == "indexObjectR1")
            {
                indexObject[1] = hand;
            }
            else if (hand.name == "indexObjectR2")
            {
                indexObject[2] = hand;
            }
            else if (hand.name == "middleObjectR")
            {
                middleObject[0] = hand;
            }
            else if (hand.name == "middleObjectR1")
            {
                middleObject[1] = hand;
            }
            else if (hand.name == "middleObjectR2")
            {
                middleObject[2] = hand;
            }
            else if (hand.name == "ringObjectR")
            {
                ringObject[0] = hand;
            }
            else if (hand.name == "ringObjectR1")
            {
                ringObject[1] = hand;
            }
            else if (hand.name == "ringObjectR2")
            {
                ringObject[2] = hand;
            }
            else if (hand.name == "pinkyObjectR")
            {
                pinkyObject[0] = hand;
            }
            else if (hand.name == "pinkyObjectR1")
            {
                pinkyObject[1] = hand;
            }
            else if (hand.name == "pinkyObjectR2")
            {
                pinkyObject[2] = hand;
            }
            else if (hand.name == "wristObjectR")
            {
                wristObject = hand;
            }
        }

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
    }
}