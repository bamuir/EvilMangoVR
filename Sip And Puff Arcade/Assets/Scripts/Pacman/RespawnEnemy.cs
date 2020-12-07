using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    float time;
    bool dead;


    List<Vector3> initialPositions;

    List<GameObject> enemies;
    public List<GameObject> enemiesToReset;

    public delegate void ResetWaypoint(GameObject enemy);
    public static event ResetWaypoint Reset;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        RespawnPlayer.Respawn += RespawnEnemies;
        ResetGame.ResetEnemies += ResetEnemies;
        enemies = new List<GameObject>();
        initialPositions = new List<Vector3>();

        for (int i = 0; i < enemiesToReset.Count; i++)
        {
            initialPositions.Add(enemiesToReset[i].transform.position);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        time += Time.time;
        if (dead && time >= 5000)
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

    void ResetEnemies()
    {
        for (int i = 0; i < enemiesToReset.Count; i++)
        {
            enemiesToReset[i].transform.position = initialPositions[i];
            enemiesToReset[i].SetActive(true);
            Reset(enemiesToReset[i]);
        }
        enemies.Clear();
        dead = false;
    }
}
