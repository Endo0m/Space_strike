using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button newGameButton;
    public Button exitButton;
    void Start()
    {
        newGameButton.onClick.AddListener(NewGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void NewGame()
    {
        ScoreManager.instance.ResetScore(); // �������� ����
        SceneManager.LoadScene(1); // ��������� ����� ������� �����
    }

    void ExitGame()
    {
        Application.Quit();
    }
}