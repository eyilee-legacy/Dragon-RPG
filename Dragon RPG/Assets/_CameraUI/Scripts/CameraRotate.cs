using System;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Transform hPivot;
    [SerializeField] private Transform vPivot;

    [Range(0.1f, 2f)]
    [SerializeField] private float xRotateSpeed = 1f;

    [Range(0.1f, 2f)]
    [SerializeField] private float yRotateSpeed = 1f;

    private Vector3 xRotateVector = Vector3.up;
    private Vector3 yRotateVector = Vector3.left;

    private void Awake()
    {
        xRotateVector *= xRotateSpeed;
        yRotateVector *= yRotateSpeed;
        ResetPivot();
    }

    private void Update()
    {
        RotateCameraRig();
    }

    private void ResetPivot()
    {
        hPivot.localRotation = Quaternion.Euler(0f, 0f, 0f);
        vPivot.localRotation = Quaternion.Euler(30f, 0f, 0f);
    }

    private void RotateCameraRig()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            hPivot.Rotate(xRotateVector * -1, Space.Self);
        }

        if (Input.GetKey(KeyCode.E))
        {
            hPivot.Rotate(xRotateVector, Space.Self);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            hPivot.Rotate(xRotateVector * x, Space.Self);
            vPivot.Rotate(yRotateVector * y, Space.Self);
        }
    }
}
