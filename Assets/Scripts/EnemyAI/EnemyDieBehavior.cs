using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieBehavior : Health, IDieBehavior
{
    [SerializeField] private float deathAnimationDuration = 1.5f; // Примерное время анимации смерти

    public override void Die()
    {
        base.Die();
        // Предполагаем, что здесь у вас уже вызывается анимация смерти
        // Вместо мгновенного уничтожения, добавляем задержку
        Invoke(nameof(DestroyObject), deathAnimationDuration);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
