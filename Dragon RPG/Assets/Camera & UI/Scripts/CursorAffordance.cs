using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [Header("Cursor Textures")]
    [SerializeField] private Texture2D targetCursor = null;
    [SerializeField] private Texture2D unknownCursor = null;
    [SerializeField] private Texture2D walkCursor = null;

    [SerializeField] private const int walkabeLayerNumber = 8;
    [SerializeField] private const int enemyLayerNumber = 9;

    private CameraRaycaster cameraRaycaster;

    private void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChange;
    }

    private void OnLayerChange (int layer) {
        switch (layer) {
            case walkabeLayerNumber:
                Cursor.SetCursor(walkCursor, Vector2.zero, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(targetCursor, Vector2.zero, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unknownCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }
}
