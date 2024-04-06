using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private string obstacleTag = "Ground";

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageAmount, transform.position);

            }

            if (health.CurrentHealth <= 0)
            {
                // Вызовите Die из IKillable, если объект должен умереть
                IDieBehavior killableComponent = collision.GetComponent<IDieBehavior>();
                if (killableComponent != null)
                {
                    killableComponent.Die();
                }
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag(obstacleTag))
        {
            Destroy(gameObject);
        }
    }

}
