using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private float countdownTime = 300f; 
    private bool timerRunning = false;

    private void Start()
    {
        timerRunning = true; 
    }

    private void Update()
    {
        if (timerRunning)
        {
            countdownTime -= Time.deltaTime;

            string minutes = Mathf.Floor(countdownTime / 60).ToString("00");
            string seconds = (countdownTime % 60).ToString("00");

            timerText.text = minutes + ":" + seconds;

            if (countdownTime <= 60f)
            {
                timerText.text = seconds + " сек";
            }

            if (countdownTime <= 0f)
            {
                countdownTime = 0f;
                timerText.text = "Время вышло!";
                timerRunning = false;
               
                 SceneManager.LoadScene("Win");
            }
        }
    }
}