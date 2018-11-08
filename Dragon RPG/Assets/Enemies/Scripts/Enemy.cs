using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float maxHealthPoint = 100;

    private float currentHealthPoint = 100;

    public float HealthAsPercentage {
        get {
            if (maxHealthPoint > currentHealthPoint) {
                return currentHealthPoint / maxHealthPoint;
            } else {
                return 1f;
            }
        }
    }
}
