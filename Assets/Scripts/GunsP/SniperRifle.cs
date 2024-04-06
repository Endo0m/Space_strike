using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : WeaponBase
{
    // Здесь переопределяйте нужные атрибуты и методы для снайперской винтовки
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
        base.Start(); // Вызов метода базового класса, если необходимо
        weaponType = WeaponType.sniperRifle;
    }

    public override void Shoot()
    {
        if (Ammo > 0 && !isReloading)
        {
            base.Shoot();
            Ammo--; // Уменьшаем количество пуль
            animator.SetTrigger("Shoot");
            UpdateAmmoUI(); // Обновляем UI после выстрела

            CheckReload(); // Проверяем, нужно ли перезарядиться после выстрела
            // Получаем позицию курсора в мировых координатах
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            // Направление от точки стрельбы к курсору
            Vector2 direction = (mousePosition - firePoint.position).normalized;

            // Установка угла поворота пули
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Вычитаем 90 градусов, если ваша пуля ориентирована вертикально в префабе

            // Создаем пулю с учетом угла поворота в направлении курсора
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));

            // Применяем силу для движения пули в указанном направлении
            bulletObject.GetComponent<Rigidbody2D>().AddForce(direction * 200f, ForceMode2D.Impulse); // Значение силы нужно настроить под вашу игру

            // Дополнительно: звук выстрела, муляж отдачи оружия и т.д.
        }
        // Если патронов нет: анимация/звук перезарядки, уведомление для игрока
    }

}