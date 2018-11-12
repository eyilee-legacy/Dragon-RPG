using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageCaused;
    public float projectileSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));

        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);
        }

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Destroy(gameObject);
    }
}
