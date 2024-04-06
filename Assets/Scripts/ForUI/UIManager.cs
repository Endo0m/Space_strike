using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text ammoText; // Убедитесь, что этот компонент назначен в редакторе Unity!
    public Text magazinesText; // Убедитесь, что этот компонент назначен в редакторе Unity!
    public Text scoreText;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

       // DontDestroyOnLoad(gameObject); // Делаем UIManager постоянным между сценами
    }
   
    void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreUI;
        UpdateScoreUI(ScoreManager.instance.Score);
    }

    void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScoreUI;
    }
    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }
    public void UpdateAmmoUIForTeslaShock()
    {
        ammoText.text = "Ammo: ∞";
        magazinesText.text = "Magazines: ∞";
    }

    public void UpdateAmmoUI(int ammo, int magazines)
    {
        ammoText.text = $"Ammo: {ammo}";
        magazinesText.text = $"Magazines: {magazines}";
    }
}