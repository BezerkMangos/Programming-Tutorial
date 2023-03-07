using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string nameCollectable;
    public int score;
    public int restoreHp;

    public Collectable(string name, int scoreVal, int restoreHpVal)
    {
        this.name = name;
        this.score = scoreVal;
        this.restoreHp = restoreHpVal;
    }

    public void UpdateScore()
    {
        ScoreManager.scoreManager.updateScore(score);
    }

    public void UpdateHealth()
    {
        
    }







}