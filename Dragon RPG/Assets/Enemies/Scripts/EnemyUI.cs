using UnityEngine;

public class EnemyUI : MonoBehaviour {

    [Tooltip("The UI canvas prefab")]
    [SerializeField] private GameObject enemyCanvasPrefab = null;

    private Camera cameraToLookAt;

    private void Start () {
        cameraToLookAt = Camera.main;
        Instantiate(enemyCanvasPrefab, transform.position, Quaternion.identity, transform);
    }

    private void LateUpdate () {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}