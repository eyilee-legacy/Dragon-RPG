using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

    [SerializeField] private float xRotationsPerMinute = 0f;
    [SerializeField] private float yRotationsPerMinute = 0f;
    [SerializeField] private float zRotationsPerMinute = 0f;

    void Update () {
        float xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationsPerMinute;
        transform.RotateAround(transform.position, transform.right, xDegreesPerFrame);

        float yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationsPerMinute;
        transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationsPerMinute;
        transform.RotateAround(transform.position, transform.forward, zDegreesPerFrame);
    }
}
