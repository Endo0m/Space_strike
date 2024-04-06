using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DieAndRespawn(other.gameObject));
        }
    }

    private IEnumerator DieAndRespawn(GameObject player)
    {
        player.SetActive(false);
        yield return new WaitForSeconds(0.5f); 
        player.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}