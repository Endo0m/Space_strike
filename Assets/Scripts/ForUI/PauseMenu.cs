using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Text timerText;
    private bool isPaused = false;
    private float savedTimeScale;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = savedTimeScale;
        isPaused = false;
    }

   public void Pause()
    {
        pauseMenuUI.SetActive(true);
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        timerText.text = "Time: " + Time.time;  // Отображение времени на паузе (можно изменить на свой текст)
        isPaused = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Заменить "MainMenu" на имя сцены главного меню
    }
}
