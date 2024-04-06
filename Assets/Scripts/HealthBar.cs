using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthUI
{
    [SerializeField] private Health healthComponent;
    [SerializeField] private Image hpBar;

    private void Awake()
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged += UpdateHealthUI;
        }
    }

    public void UpdateHealthUI(float healthPercentage)
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = healthPercentage;
        }
    }

    private void OnDestroy()
    {
        if (healthComponent != null) // Отписываемся, чтобы избежать утечек памяти
        {
            healthComponent.OnHealthChanged -= UpdateHealthUI;
        }
    }
}
