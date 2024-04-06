using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieBehavior : Health, IDieBehavior
{
    [SerializeField] private float deathAnimationDuration = 1.5f; // ��������� ����� �������� ������

    public override void Die()
    {
        base.Die();
        // ������������, ��� ����� � ��� ��� ���������� �������� ������
        // ������ ����������� �����������, ��������� ��������
        Invoke(nameof(DestroyObject), deathAnimationDuration);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
