using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private const int walkabeLayerNumber = 8;
    private const int enemyLayerNumber = 9;

    [SerializeField] private float maxHealthPoint = 100f;

    float attackSpeed = 0.5f;
    float attackDamage = 10f;
    float attackRange = 2f;
    float lastHittime = 0f;

    GameObject currentTarget;
    CameraRaycaster cameraRaycaster;

    private float currentHealthPoint;

    public float HealthAsPercentage { get { return currentHealthPoint / maxHealthPoint; } }

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        currentHealthPoint = maxHealthPoint;
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoint = Mathf.Clamp(currentHealthPoint - damage, 0f, currentHealthPoint);
    }

    private void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayerNumber)
        {
            GameObject enemy = raycastHit.collider.gameObject;
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy > attackRange)
            {
                return;
            }
            currentTarget = enemy;
            Enemy enemyComponemt = enemy.GetComponent<Enemy>();
            if (Time.time - lastHittime > attackSpeed)
            {
                enemyComponemt.TakeDamage(attackDamage);
                lastHittime = Time.time;
            }
        }
    }
}
