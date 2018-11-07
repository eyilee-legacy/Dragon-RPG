using UnityEngine;

public class CameraRaycaster : MonoBehaviour {
    public readonly Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] private float distanceToBackground = 100f;
    private Camera viewCamera;
    private RaycastHit _hit;
    public RaycastHit Hit {
        get { return _hit; }
    }

    private Layer _layer;
    public Layer LayerHit {
        get { return _layer; }
    }

    public delegate void OnLayerChange (Layer layer);
    public event OnLayerChange OnLayerChangeObserver;

    private void Start () {
        viewCamera = Camera.main;
    }

    private void Update () {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities) {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue) {
                _hit = hit.Value;
                if (layer != LayerHit) {
                    _layer = layer;
                    OnLayerChangeObserver(layer);
                }
                return;
            }
        }

        // Otherwise return background hit
        _hit.distance = distanceToBackground;
        _layer = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer (Layer layer) {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit) {
            return hit;
        }
        return null;
    }
}
