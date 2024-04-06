using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaShock : WeaponBase
{
    public GameObject flameEffectPrefab; // Префаб для визуализации огня
    private GameObject currentFlameEffect; // Текущий активный эффект огня

    public TeslaShock()
    {

        Damage = 200f; // У огнемёта может быть меньше урона, но непрерывное нанесение
        FireRate = 0f; // Может быть игнорировано для непрерывного оружия
        ReloadTime = 3f; // Возможно, нужно больше времени для "перезарядки" топлива
        Ammo = 999; // Представляем как емкость топлива
        Magazines = 999; // Количество запасных "баллонов с топливом"
    }

    protected override void Start()
    {
        base.Start();
        isTeslaShock = true;
        weaponType = WeaponType.assaultRifle; // Или создать новый тип для огнемёта
    }

    public override void Shoot()
    {
        if (Ammo > 0 && !isReloading)
        {
            if (currentFlameEffect == null)
            {
                audioSource.Play();
                // Рассчитаем направление и угол к курсору.
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle); 

                // Создание эффекта пламени с корректировкой угла поворота.
                currentFlameEffect = Instantiate(flameEffectPrefab, firePoint.position, rotation, firePoint);

                // Потенциально, начать уменьшение Ammo здесь
            }
        }
        else
        {
            StopFlame();
        }
    }


    new void Update()
    {
        base.Update(); // Вызов базового метода Update.

        


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

    // Переопределение Reload для специфической логики огнемёта (если нужно)
    public override IEnumerator Reload()
    {
        // Специфическая логика перезарядки для огнемёта
        yield return base.Reload();
    }
}