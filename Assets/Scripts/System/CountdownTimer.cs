using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    private float timeRemaining = 120f;
    private bool timerIsRunning = false;

    void Start()
    {
        timerIsRunning = true;
        UpdateCountdownText();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownText();
            }
            else
            {
                // 타이머가 끝나면 게임씬으로 전환 (또는 다른 액션)
                timeRemaining = 0;
                timerIsRunning = false;
                OnCountdownFinished();
            }
        }
    }

    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnCountdownFinished()
    {
        SceneManager.LoadScene("EndingScene");
    }
}