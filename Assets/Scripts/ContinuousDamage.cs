using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 25f; 
    [SerializeField] private float damageInterval = 1f; 
    private float nextDamageTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time >= nextDamageTime)
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageAmount, transform.position);

                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
