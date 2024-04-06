using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IGun
{
    public float Damage { get; protected set; }
    public float FireRate { get; protected set; }
    public float ReloadTime { get; protected set; }
    public int Ammo { get; set; }
    public int Magazines { get; set; }
    public int MaxAmmo { get;  set; }


    public GameObject bulletPrefab;
    public GameObject reloadImg;
    public Transform firePoint;
    public Animator animator;
    protected UIManager uiManager;
    public WeaponType weaponType;
    public AudioClip shootSound; // Аудиоклип для звука выстрела
    protected AudioSource audioSource;
    public bool IsReloading => isReloading;



    public bool isTeslaShock = false; // Флаг, указывающий является ли оружие TeslaShock

    protected float nextFire = 0f;
    protected bool isReloading = false;
    protected virtual void Start()
    {
        uiManager = UIManager.Instance; // Assume UIManager follows singleton pattern.
        UpdateAmmoUI(); // Первоначальное обновление UI при старте.
                        // Загрузка источника аудио
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // Если забыли добавить компонент AudioSource, добавляем его во время выполнения
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    protected virtual void UpdateAmmoUI()
    {
        if (uiManager != null)
        {
            if (isTeslaShock)
            {
                uiManager.UpdateAmmoUIForTeslaShock();
            }
            else
            {
                uiManager.UpdateAmmoUI(Ammo, Magazines);
            }
        }
    }

    protected virtual void CheckReload()
    {
        if (Ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }
    public virtual void Shoot()
    {
        // Общая логика для выстрела
        // Воспроизведение звука выстрела
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public virtual IEnumerator Reload()
    {
        if (Magazines > 0 && !isReloading)
        {
            isReloading = true;
            reloadImg.gameObject.SetActive(true);
            yield return new WaitForSeconds(ReloadTime);

            Magazines -= 1;
            Ammo = MaxAmmo; // Использование MaxAmmo
            isReloading = false;
            reloadImg.gameObject.SetActive(false);

            UpdateAmmoUI();
        }
    }

    protected void Update()
    {
        if (!isReloading && Ammo > 0)
        {
            if (Input.GetMouseButton(0) && Time.time >= nextFire)
            {
                nextFire = Time.time + FireRate;
                Shoot();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }
        UpdateAmmoUI();

    }
}