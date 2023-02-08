using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int score = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ScoreManager.scoreManager.updateScore(score);
            Destroy(gameObject);

            
        }

    }

}
