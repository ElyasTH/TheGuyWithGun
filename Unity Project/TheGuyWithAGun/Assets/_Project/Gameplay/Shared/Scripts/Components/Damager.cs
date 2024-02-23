using System;
using Unity.VisualScripting;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float damageForce;
    [SerializeField] private float damageUpwardForce;
    
    public void Damage(Damageable damageable)
    {
        damageable.TakeDamage(damageAmount);
        
        if (damageable.hasForceOnHit())
        {
            var direction = (damageable.transform.position - transform.position).normalized;
            damageable.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(direction.x * damageForce, damageUpwardForce), ForceMode2D.Impulse);
        }
    }
}
