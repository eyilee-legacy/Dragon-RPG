using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] float walkMoveStopRadius = 0.2f;

    private ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentClickTarget;

    private bool isInDirectMode = false;

    private void Start () {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.G)) {
            isInDirectMode = !isInDirectMode;
            currentClickTarget = transform.position;
        }

        if (isInDirectMode) {
            ProcessDirectMovement();
        } else {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    private void ProcessMouseMovement () {
        if (Input.GetMouseButton(0)) {
            switch (cameraRaycaster.LayerHit) {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.Hit.point;
                    break;
                case Layer.Enemy:
                    break;
                case Layer.RaycastEndStop:
                    break;
                default:
                    break;
            }
        }

        Vector3 playerToClickPoint = currentClickTarget - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        } else {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }
}

