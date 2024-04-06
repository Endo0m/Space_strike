using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : WeaponBase
{
   
    public AssaultRifle()
    {
        Damage = 5f;
        FireRate = 0.1f;
        ReloadTime = 2f;
        Ammo = 50;
        Magazines = 5;
        MaxAmmo = 50;
    }
    protected override void Start()
    {
        base.Start(); // ����� ������ �������� ������, ���� ����������
        weaponType = WeaponType.assaultRifle;
    }
    public override void Shoot()
    {
        if (Ammo > 0 && !isReloading)
        {
            base.Shoot();
            Ammo--; // ��������� ���������� ����
            animator.SetTrigger("Shoot");
            UpdateAmmoUI(); // ��������� UI ����� ��������

            CheckReload(); // ���������, ����� �� �������������� ����� ��������

            // �������� ������� ������� � ������� �����������
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            // ����������� �� ����� �������� � �������
            Vector2 direction = (mousePosition - firePoint.position).normalized;

            // ��������� ���� �������� ����
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // �������� 90 ��������, ���� ���� ���� ������������� ����������� � �������

            // ������� ���� � ������ ���� �������� � ����������� �������
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));

            // ��������� ���� ��� �������� ���� � ��������� �����������
            bulletObject.GetComponent<Rigidbody2D>().AddForce(direction * 100f, ForceMode2D.Impulse); // �������� ���� ����� ��������� ��� ���� ����

            // �������������: ���� ��������, ����� ������ ������ � �.�.
        }
        // ���� �������� ���: ��������/���� �����������, ����������� ��� ������
    }
}

