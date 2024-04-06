using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaShock : WeaponBase
{
    public GameObject flameEffectPrefab; // ������ ��� ������������ ����
    private GameObject currentFlameEffect; // ������� �������� ������ ����

    public TeslaShock()
    {

        Damage = 200f; // � ������� ����� ���� ������ �����, �� ����������� ���������
        FireRate = 0f; // ����� ���� ������������ ��� ������������ ������
        ReloadTime = 3f; // ��������, ����� ������ ������� ��� "�����������" �������
        Ammo = 999; // ������������ ��� ������� �������
        Magazines = 999; // ���������� �������� "�������� � ��������"
    }

    protected override void Start()
    {
        base.Start();
        isTeslaShock = true;
        weaponType = WeaponType.assaultRifle; // ��� ������� ����� ��� ��� �������
    }

    public override void Shoot()
    {
        if (Ammo > 0 && !isReloading)
        {
            if (currentFlameEffect == null)
            {
                audioSource.Play();
                // ���������� ����������� � ���� � �������.
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle); 

                // �������� ������� ������� � �������������� ���� ��������.
                currentFlameEffect = Instantiate(flameEffectPrefab, firePoint.position, rotation, firePoint);

                // ������������, ������ ���������� Ammo �����
            }
        }
        else
        {
            StopFlame();
        }
    }


    new void Update()
    {
        base.Update(); // ����� �������� ������ Update.

        


        if (Input.GetMouseButtonUp(0))
        {
            StopFlame();
        }
    }

    void StopFlame()
    {
        
            if (currentFlameEffect != null)
            {
                Destroy(currentFlameEffect); // Remove flame effect.
                currentFlameEffect = null;
                audioSource.Stop(); // Stop the continuous firing sound.
            }
        
    }

    // ��������������� Reload ��� ������������� ������ ������� (���� �����)
    public override IEnumerator Reload()
    {
        // ������������� ������ ����������� ��� �������
        yield return base.Reload();
    }
}