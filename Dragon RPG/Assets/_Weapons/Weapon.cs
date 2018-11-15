using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Weapon")]
public class Weapon : ScriptableObject
{
    public Transform gripTransform;

    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackRange = 2f;

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public GameObject GetWeaponPrefab()
    {
        return weaponPrefab;
    }

    public AnimationClip GetAttackAnimation()
    {
        RemoveAnimationEvents();
        return attackAnimation;
    }

    private void RemoveAnimationEvents()
    {
        attackAnimation.events = new AnimationEvent[0];
    }
}
