using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndLives : MonoBehaviour
{
    int totalScore;
    public Text scoreText;
    public Text livesText;


    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        scoreText.text = "0";
        RespawnPlayer.Score += IncreaseScore;
        RespawnPlayer.Lives += UpdateLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncreaseScore(int score)
    {
        totalScore += score;
    }

    void UpdateLives(int currentLives)
    {
        livesText.text = currentLives.ToString();
    }
}
