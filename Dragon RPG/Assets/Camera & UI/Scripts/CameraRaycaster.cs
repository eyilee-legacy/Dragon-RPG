using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    private readonly Layer[] LayerPriorities = {
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
        foreach (Layer layer in LayerPriorities) {
            RaycastHit? raycastHit = RaycastForLayer(layer);
            if (raycastHit.HasValue) {
                _hit = raycastHit.Value;

                if (layer != _layer) {
                    _layer = layer;
                    OnLayerChangeObserver(_layer);
                }

                return;
            }
        }

        _layer = Layer.RaycastEndStop;
        OnLayerChangeObserver(_layer);
    }

    RaycastHit? RaycastForLayer (Layer layer) {
        RaycastHit raycastHit;
        bool hasHit = Physics.Raycast(
            viewCamera.ScreenPointToRay(Input.mousePosition),
            out raycastHit,
            distanceToBackground,
            1 << (int)layer
        );

        if (hasHit) {
            return raycastHit;
        }

        return null;
    }
}
