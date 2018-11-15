using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 heightAdjust = Vector3.up * 1.2f;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        transform.position = player.position + heightAdjust;
    }
}
