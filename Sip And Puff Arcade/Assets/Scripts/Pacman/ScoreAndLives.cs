using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndLives : MonoBehaviour
{
    int totalScore;
    public Text scoreText;
    public Text livesText;


    private void OnEnable()
    {
        RespawnPlayer.Score += IncreaseScore;
        RespawnPlayer.Lives += UpdateLives;
        ResetGame.ResetLivesAndScore += ResetInfo;
    }

    private void OnDisable()
    {
        RespawnPlayer.Score -= IncreaseScore;
        RespawnPlayer.Lives -= UpdateLives;
        ResetGame.ResetLivesAndScore -= ResetInfo;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        scoreText.text = "0";
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = totalScore.ToString();
    }

    void IncreaseScore(int score)
    {
        totalScore += score;
    }

    void UpdateLives(int currentLives)
    {
        livesText.text = currentLives.ToString();
    }

    void ResetInfo()
    {
        totalScore = 0;
        scoreText.text = "0";
        //UpdateLives(0);
    }
}
