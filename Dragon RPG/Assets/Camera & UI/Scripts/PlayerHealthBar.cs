using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour {

    RawImage healthBarRawImage;
    Player player;

    private void Start () {
        player = FindObjectOfType<Player>();
        healthBarRawImage = GetComponent<RawImage>();
    }

    private void Update () {
        float xValue = 0.5f * (1 - player.HealthAsPercentage);
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
