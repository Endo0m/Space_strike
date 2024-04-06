using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform[] spawnPoints; 
    [SerializeField]
    private float respawnTime = 2f; 
    private bool playerIsInZone = false;
    IEnumerator SpawnEnemies()
    {
        while (playerIsInZone)
        {
            yield return new WaitForSeconds(respawnTime);

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerIsInZone) 
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
        }
    }
}