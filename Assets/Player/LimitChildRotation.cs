using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitChildRotation : MonoBehaviour
{
    public GameObject child;
    public float minY = -.6f;
    public float maxY = .6f;
    public float rotationSpeed = 500.0f;

    void Update()
    {
        float childY = child.transform.localRotation.y;
        if (childY < minY || childY > maxY)
        {
            float direction = childY > 0 ? 1.0f : -1.0f;
            transform.Rotate(new Vector3(0, direction * rotationSpeed * Time.deltaTime, 0));
        }
    }
}