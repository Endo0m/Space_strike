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
    private GameObject currentpickupPref = null; // ���������� ��� �������� ������ �� ���������� ���������� 
    IEnumerator SpawnEnemies()
    {
        while (playerIsInZone)
        {
            if (currentpickupPref == null || !currentpickupPref.activeInHierarchy) // ���������, ���� �� �������� 
            {
                yield return new WaitForSeconds(respawnTime);
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //����� ��������� ����� ���������
                currentpickupPref = Instantiate(pickupPrefub, spawnPoint.position, Quaternion.identity); //�������� � ���������� ������ �� ����
            }
            else
            {
                yield return null; // ����  ��� ���� � �� �������, ���� ���������� ����� ����� ��������� ���������
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerIsInZone) //������ ���� ����� ������� ������ � ����
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
            // ����� ��������� �������� �� ���������, ��� ��� ��� ����������� ���� ��-�� ������� �����
        }
    }
}