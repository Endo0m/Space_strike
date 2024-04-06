using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private Health healthComponent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        healthComponent = GetComponent<Health>();

        // Подписываемся на события из Health
        healthComponent.OnTakeDamage += HandleTakeDamage;
        healthComponent.OnDie += HandleDie;
    }

    private void HandleTakeDamage(Vector3 attackerPosition)
    {
        Vector3 toAttacker = (attackerPosition - transform.position).normalized;
        bool isFromBack = Vector3.Dot(toAttacker, transform.forward) < 0;

        if (isFromBack)
        {
            animator.SetTrigger("HitFromBack");
        }
        else
        {
            animator.SetTrigger("HitFromFront");
        }
    }

    private void HandleDie()
    {
        animator.SetTrigger("Die");
    }
}
