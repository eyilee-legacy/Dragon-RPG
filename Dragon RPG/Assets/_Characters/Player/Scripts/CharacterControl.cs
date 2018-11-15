using System;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterControl : MonoBehaviour
{
    private Character _Character;
    private Transform cameraTransform;
    private bool jump;

    private void Awake()
    {
        _Character = GetComponent<Character>();
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 cameraForword = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * cameraForword + h * cameraTransform.right;

        _Character.Move(move, jump);

        jump = false;
    }
}
