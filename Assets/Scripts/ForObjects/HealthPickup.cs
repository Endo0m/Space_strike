using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healthAmount = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Проверяем, является ли объект игроком
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.Heal(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}