using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStretch : MonoBehaviour
{
    [SerializeField] private float minDistance = 4f;
    [SerializeField] private float maxDistance = 12f;
    [SerializeField] private float currentDistance = 8f;

    [SerializeField] private float stretchFactor = 0.25f; // stretch distance per scroll

    private void Awake()
    {
        transform.localPosition = Vector3.back * currentDistance;
    }

    private void Update()
    {
        StretchCameraRig();
    }

    private void StretchCameraRig()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(wheel) > Mathf.Epsilon)
        {
            float deltaDistance = Mathf.Sign(wheel) * stretchFactor * -1;
            currentDistance = Mathf.Clamp(currentDistance + deltaDistance, minDistance, maxDistance);
            transform.localPosition = Vector3.back * currentDistance;
        }
    }
}
