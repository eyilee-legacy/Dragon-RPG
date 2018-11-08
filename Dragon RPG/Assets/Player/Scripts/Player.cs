using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private int maxHealthPoint = 100;

    private int currentHealthPoint = 100;

    public float healthAsPercentage {
        get {
            if (maxHealthPoint > currentHealthPoint) {
                return currentHealthPoint / (float)maxHealthPoint;
            } else {
                return 1;
            }
        }
    }
}
