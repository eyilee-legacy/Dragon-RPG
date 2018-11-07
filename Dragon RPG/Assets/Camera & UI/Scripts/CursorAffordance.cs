using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [Header("Cursor Textures")]
    [SerializeField] private Texture2D targetCursor = null;
    [SerializeField] private Texture2D unknownCursor = null;
    [SerializeField] private Texture2D walkCursor = null;

    [Header("Other Setting")]
    [SerializeField] private Vector2 cursorHotSpot = Vector2.zero;

    private CameraRaycaster cameraRaycaster;

    private void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.OnLayerChangeObserver += OnLayerChange;
    }

    private void OnLayerChange (Layer layer) {
        switch (layer) {
            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(targetCursor, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                break;
        }
    }
}
