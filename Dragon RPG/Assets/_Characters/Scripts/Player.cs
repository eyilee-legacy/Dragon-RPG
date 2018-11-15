using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour, IDamageable
{
    private const int walkabeLayerNumber = 8;
    private const int enemyLayerNumber = 9;

    [SerializeField] private float maxHealthPoint = 100f;
    [SerializeField] Weapon weaponInUse;
    [SerializeField] AnimatorOverrideController animatorOverrideController;

    float attackDamage = 10f;
    float lastHittime = 0f;

    CameraRaycaster cameraRaycaster;
    Animator animator;
    private float currentHealthPoint;

    public float HealthAsPercentage { get { return currentHealthPoint / maxHealthPoint; } }

    private void Start()
    {
        RegisterMouseClick();
        SetCurrentMaxHealth();
        PutWeaponInHand();
        SetupRuntimeAnimator();
    }

    private void SetCurrentMaxHealth()
    {
        currentHealthPoint = maxHealthPoint;
    }

    private void SetupRuntimeAnimator()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["Attack"] = weaponInUse.GetAttackAnimation();
    }

    private void PutWeaponInHand()
    {
        GameObject weaponPrefab = weaponInUse.GetWeaponPrefab();
        var weaponSocket = RequestDominantHand();
        GameObject weapon = Instantiate(weaponPrefab, weaponSocket.transform);
        weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
        weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
    }

    private GameObject RequestDominantHand()
    {
        DominantHand[] dominantHands = GetComponentsInChildren<DominantHand>();
        int numberOfDominantHands = dominantHands.Length;
        Assert.AreNotEqual(numberOfDominantHands, 0, "No DominantHand found on Player, Please add one.");
        Assert.IsFalse(numberOfDominantHands > 1, "Multiple DominantHand scripts on Player, Please remove one.");
        return dominantHands[0].gameObject;
    }

    private void RegisterMouseClick()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
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

            if (IsTargetInRange(enemy))
            {
                AttackTarget(enemy);
            }

        }
    }

    private bool IsTargetInRange(GameObject target)
    {
        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
        return distanceToEnemy < weaponInUse.GetAttackRange();
    }

    private void AttackTarget(GameObject target)
    {
        Enemy enemyComponemt = target.GetComponent<Enemy>();
        if (Time.time - lastHittime > weaponInUse.GetAttackSpeed())
        {
            animator.SetTrigger("Attack");
            enemyComponemt.TakeDamage(attackDamage);
            lastHittime = Time.time;
        }
    }
}
