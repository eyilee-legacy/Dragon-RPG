using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    private CameraRaycaster cameraRaycaster;
    private ThirdPersonCharacter thirdPersonCharacter;
    private Vector3 currentDestination;
    private Vector3 destination;

    private bool isInDirectMode = false;

    private void Start () {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    private void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.G)) {
            isInDirectMode = !isInDirectMode;
            currentDestination = transform.position;
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
            destination = cameraRaycaster.Hit.point;
            switch (cameraRaycaster.LayerHit) {
                case Layer.Walkable:
                    currentDestination = ShortDestination(destination, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(destination, attackMoveStopRadius);
                    break;
                case Layer.RaycastEndStop:
                    break;
                default:
                    break;
            }
        }

        WalkToDestination();
    }

    private Vector3 ShortDestination (Vector3 destination, float shortening) {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void WalkToDestination () {
        Vector3 toDestination = currentDestination - transform.position;
        if (toDestination.magnitude >= walkMoveStopRadius) {
            thirdPersonCharacter.Move(toDestination, false, false);
        } else {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, destination);
        Gizmos.DrawSphere(destination, 0.1f);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

