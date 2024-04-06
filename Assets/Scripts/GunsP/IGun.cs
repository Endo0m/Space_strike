using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    float Damage { get; }
    float FireRate { get; }
    float ReloadTime { get; }
    int Ammo { get; set; }
    int Magazines { get; set; }
    int MaxAmmo { get; set; }

    void Shoot();
    IEnumerator Reload();
}
public enum WeaponType
{
    sniperRifle,
    teslaShock,
    assaultRifle
}