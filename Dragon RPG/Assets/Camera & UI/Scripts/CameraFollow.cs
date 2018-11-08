using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;

    private void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate () {
        transform.position = player.transform.position;
    }
}
