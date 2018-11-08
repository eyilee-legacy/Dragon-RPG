using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [Header("Cursor Textures")]
    [SerializeField] private Texture2D targetCursor = null;
    [SerializeField] private Texture2D unknownCursor = null;
    [SerializeField] private Texture2D walkCursor = null;

    private CameraRaycaster cameraRaycaster;

    private void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.OnLayerChangeObserver += OnLayerChange;
    }

    private void OnLayerChange (Layer layer) {
        switch (layer) {
            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, Vector2.zero, CursorMode.Auto);
                break;
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, Vector2.zero, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(targetCursor, Vector2.zero, CursorMode.Auto);
                break;
            default:
                break;
        }
    }
}
