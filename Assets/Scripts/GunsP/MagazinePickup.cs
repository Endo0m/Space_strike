using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinePickup : MonoBehaviour
{
    public int magazinesToAdd = 1;
    public WeaponType forWeaponType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Проверяем, является ли объект игроком
        {
            WeaponBase weapon = collision.gameObject.GetComponentInChildren<WeaponBase>();
            if (weapon != null && weapon.weaponType == forWeaponType)
            {
                weapon.Magazines += magazinesToAdd;
                Destroy(gameObject);
            }
        }
    }
}