using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headTracking : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera cam;
    private GameObject Neck;
    public float minAngle = -30f; // Minimum angle of rotation
    public float maxAngle = 30f; // Maximum angle of rotation


    void Start()
    {
        cam = Camera.main;
        Neck = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Copy camera rotation to head

        Quaternion camRotation = cam.transform.rotation;

        transform.rotation = camRotation;


        // Rotate neck

        Quaternion neckRotation = Neck.transform.rotation;
        //Debug.Log(WrapAngle(camRotation.eulerAngles.y));


        float TempneckAngle = WrapAngle(neckRotation.eulerAngles.y);
        //Debug.Log("Diff: " + (WrapAngle(camRotation.eulerAngles.y) - TempneckAngle));
        float diff = WrapAngle(camRotation.eulerAngles.y) - TempneckAngle;
        

        if (diff > maxAngle)
        {
            //Rotate abit to the left
            neckRotation = Quaternion.Euler(0, TempneckAngle + 5, 0);
            Neck.transform.rotation = neckRotation;
        }   
        else if (diff < minAngle)
        {
            //Rotate abit to the right  
            neckRotation = Quaternion.Euler(0, TempneckAngle - 5, 0);
            Neck.transform.rotation = neckRotation;
        }
    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }
}