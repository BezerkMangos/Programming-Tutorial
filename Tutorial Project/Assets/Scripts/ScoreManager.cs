using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;
    public TextMeshProUGUI scoreUI;
    int totalScore = 0;

    private void Awake()
    {
        if (scoreManager == null)
        {
            scoreManager = this;
        }

        scoreUI.text = "Score: " + totalScore.ToString();

    }

    public void updateScore(int score)
    {
        totalScore += score; // <- totalScore = totalScore + score;
        Debug.Log(totalScore);
        scoreUI.text = "Score: " + totalScore.ToString();
    }
}
