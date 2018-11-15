using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    private Enemy enemy = null;
    private RawImage healthBarRawImage = null;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        healthBarRawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        float xValue = 0.5f * (1 - enemy.HealthAsPercentage);
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
