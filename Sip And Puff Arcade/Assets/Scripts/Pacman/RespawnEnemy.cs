using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    float time;
    bool dead;

    List<GameObject> enemies;

    public delegate void ResetWaypoint(GameObject enemy);
    public static event ResetWaypoint Reset;

    private void Awake()
    {
        RespawnPlayer.Respawn += RespawnEnemies;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        time += Time.time;
        if (dead && time >= 10000)
        {
            Reset(enemies[0]);
            enemies.RemoveAt(0);
            time = 0.0f;
            if (enemies.Count == 0) {
                dead = false;
            }
        }
    }

    void RespawnEnemies(GameObject enemy)
    {
        enemies.Add(enemy);
        //Reset(enemy);
        enemy.SetActive(false);
        time = 0.0f;
        dead = true;
    }
}
