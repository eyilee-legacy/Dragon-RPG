using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private const int walkabeLayerNumber = 8;
    private const int enemyLayerNumber = 9;

    private AICharacterControl aiCharacterControl;

    private CameraRaycaster cameraRaycaster;

    private GameObject walkTarget;

    private void Awake()
    {
        aiCharacterControl = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("walkTarget");
    }

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }

    private void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case walkabeLayerNumber:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
            case enemyLayerNumber:
                aiCharacterControl.SetTarget(raycastHit.collider.gameObject.transform);
                break;
            default:
                break;
        }
    }
}

