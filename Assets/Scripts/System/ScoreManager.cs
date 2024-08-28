using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // 기본 점수는 0
    public Text scoreText;
    
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // 점수 UI를 업데이트하는 함수
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
