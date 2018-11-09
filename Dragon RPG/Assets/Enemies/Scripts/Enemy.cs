using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

    [SerializeField] private float maxHealthPoint = 100;

    [SerializeField] private float attackMoveRadius = 5f;

    private float currentHealthPoint = 100;

    private GameObject player;
    private AICharacterControl aiCharacterControl;

    private void Start () {
        aiCharacterControl = GetComponent<AICharacterControl>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update () {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackMoveRadius) {
            aiCharacterControl.SetTarget(player.transform);
        } else {
            aiCharacterControl.SetTarget(transform);
        }
    }

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
