using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject pickupPrefub;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private float respawnTime = 2f;
    private bool playerIsInZone = false;
    private GameObject currentpickupPref = null; // ѕеременна€ дл€ хранени€ ссылки на последнего созданного 
    IEnumerator SpawnEnemies()
    {
        while (playerIsInZone)
        {
            if (currentpickupPref == null || !currentpickupPref.activeInHierarchy) // ѕровер€ем, есть ли активный 
            {
                yield return new WaitForSeconds(respawnTime);
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //¬ыбор случайной точки по€влени€
                currentpickupPref = Instantiate(pickupPrefub, spawnPoint.position, Quaternion.identity); //—оздание и сохранение ссылки на него
            }
            else
            {
                yield return null; // ≈сли  уже есть и он активен, ждем следующего кадра перед повторной проверкой
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerIsInZone) //“олько если игрок впервые входит в зону
            {
                playerIsInZone = true;
                StartCoroutine(SpawnEnemies());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInZone = false;
            // «десь остановка корутины не требуетс€, так как она прекратитс€ сама из-за услови€ цикла
        }
    }
}