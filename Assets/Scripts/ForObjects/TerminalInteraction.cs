using UnityEngine;
using UnityEngine.UI;

public class TerminalInteraction : MonoBehaviour
{
    [SerializeField] private  GameObject interactionText; // —сылка на текст UI

    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionText != null)
            {
            interactionText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }
    }
}
