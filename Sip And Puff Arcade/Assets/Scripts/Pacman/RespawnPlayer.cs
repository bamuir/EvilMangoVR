using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    Vector3 startingPos;
    public int lives = 3;
    float time = 0.0f;
    bool potion = false;
    int kills = 0;
    public int powerupScore = 5;
    public int coinScore = 1;
    public int enemyKillScore = 10;

    public delegate void RespawnEnemy(GameObject enemy);
    public static event RespawnEnemy Respawn;

    public delegate void RespawnedPlayer(Vector3 position);
    public static event RespawnedPlayer Respawned;

    public delegate void IncrementScore(int score);
    public static event IncrementScore Score;

    public delegate void IncrementLives(int lives);
    public static event IncrementLives Lives;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = gameObject.transform.position;
        Lives(lives);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.time;

        if (potion && time >= 10000)
        {
            //Debug.Log("Potion Ran Out");
            potion = false;
        }

        if (kills == 2)
        {
            lives++;
            Lives(lives);
            kills = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potion"))
        {
            //Debug.Log("Potion Drank");
            Score(powerupScore);
            potion = true;
            time = 0.0f;
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Enemy"))
        {
            if (potion)
            {
                kills++;
                Score(enemyKillScore);
                Respawn(collision.gameObject);
            }
            else
            {
                lives--;
                Lives(lives);
                if (lives <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    //gameObject.transform.position = startingPos;
                    Respawned(startingPos);
                }
            }
        }
        if (collision.CompareTag("Coin"))
        {
            Score(1);
            collision.gameObject.SetActive(false);
        }
    }
}
