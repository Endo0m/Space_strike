using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int currentWeaponIndex = 0;
    public WeaponBase[] weapons;

    void Start()
    {

        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // Запретить переключение оружия, если текущее оружие перезаряжается.
        if (weapons[currentWeaponIndex].IsReloading)
        {
            return;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            SwitchWeapon(currentWeaponIndex);
        }
        else if (scroll < 0)
        {
            currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
            SwitchWeapon(currentWeaponIndex);
        }
    }


    void SwitchWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null) // Добавьте эту проверку
            {
                weapons[i].gameObject.SetActive(i == index);
            }
        }
    }
}