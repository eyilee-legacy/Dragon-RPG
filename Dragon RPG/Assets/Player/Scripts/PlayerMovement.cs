using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    private ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentDistination;
    private Vector3 clickPoint;

    private bool isInDirectMode = false;

    private void Start () {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDistination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.G)) {
            isInDirectMode = !isInDirectMode;
            currentDistination = transform.position;
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
            clickPoint = cameraRaycaster.Hit.point;
            switch (cameraRaycaster.LayerHit) {
                case Layer.Walkable:
                    currentDistination = ShortDestination(clickPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    currentDistination = ShortDestination(clickPoint, attackMoveStopRadius);
                    break;
                case Layer.RaycastEndStop:
                    break;
                default:
                    break;
            }
        }

        WalkToDestination();
    }

    private void WalkToDestination () {
        Vector3 playerToClickPoint = currentDistination - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        } else {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    private Vector3 ShortDestination (Vector3 destination, float shortening) {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawWireSphere(clickPoint, walkMoveStopRadius);

        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

