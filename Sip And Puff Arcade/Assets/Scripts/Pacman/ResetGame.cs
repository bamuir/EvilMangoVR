using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public delegate void ResetPlayer(Vector3 startingPos);
    public static event ResetPlayer ResetPuffman;

    public delegate void ResetEnemy();
    public static event ResetEnemy ResetEnemies;

    public delegate void ResetText();
    public static event ResetText ResetLivesAndScore;

    public delegate void ResetCollectibles();
    public static event ResetCollectibles ResetCoinsAndPotions;

    Vector3 startingPos;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyComboOne))
        {
            ResetPuffman(startingPos);
            ResetEnemies();
            ResetLivesAndScore();
            ResetCoinsAndPotions();
        }
        if (!player.activeInHierarchy)
        {
            GameOverReset();
        }
    }

    void GameOverReset()
    {
        player.SetActive(true);
        ResetPuffman(startingPos);
        ResetEnemies();
        ResetLivesAndScore();
        ResetCoinsAndPotions();
    }
}
