using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator weaponAnimator;
    public AudioSource audioSource;
    public float shootingSpeed = 0.5f;
    public int maxBullets = 50;
    public int powerShoot = 50;
    private int bulletsLeft;
    public float reloadTime = 2f;
    private bool isReloading = false;
    private Transform player;
    public GameObject reloadImg;
    public float turnSpeed = 5f;
    public enum FireMode { Single, Automatic }
    public FireMode fireMode = FireMode.Single;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    private void Start()
    {
        bulletsLeft = maxBullets;
        player = GameObject.FindGameObjectWithTag("Target")?.transform; // Находим игрока по тегу 
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingSpeed);
            if (bulletsLeft > 0)
            {
                Shoot();
                bulletsLeft--;
            }
            else
            {
                StartCoroutine(Reload());
                break;
            }
        }
    }

    public void Shoot()
    {
        if (isReloading || player == null) return;
        if (fireMode == FireMode.Automatic && Time.time < nextFireTime) return;
        if (bulletsLeft > 0)
        {
            nextFireTime = Time.time + 1f / fireRate;
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("Shoot");
            }
            audioSource.Play();

            Vector2 direction = (player.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));
            bulletObject.GetComponent<Rigidbody2D>().AddForce(direction * powerShoot, ForceMode2D.Impulse);
            bulletsLeft--;

            if (bulletsLeft <= 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        reloadImg.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = maxBullets;
        isReloading = false;
        reloadImg.SetActive(false);
    }
}
