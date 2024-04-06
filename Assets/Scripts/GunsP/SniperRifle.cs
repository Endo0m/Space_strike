using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : WeaponBase
{
    // ����� ��������������� ������ �������� � ������ ��� ����������� ��������
    public SniperRifle()
    {
        Damage = 20f;
        FireRate = 3f;
        ReloadTime = 2f;
        Ammo = 10;
        Magazines = 5;
        MaxAmmo = 10;
    }
    protected override void Start()
    {
        base.Start(); // ����� ������ �������� ������, ���� ����������
        weaponType = WeaponType.sniperRifle;
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
            bulletObject.GetComponent<Rigidbody2D>().AddForce(direction * 200f, ForceMode2D.Impulse); // �������� ���� ����� ��������� ��� ���� ����

            // �������������: ���� ��������, ����� ������ ������ � �.�.
        }
        // ���� �������� ���: ��������/���� �����������, ����������� ��� ������
    }

}