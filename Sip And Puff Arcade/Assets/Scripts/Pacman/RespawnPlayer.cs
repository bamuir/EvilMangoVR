using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    Vector3 startingPos;
    public int lives = 3;
    float time = 0.0f;
    bool potion = false;

    public delegate void RespawnEnemy(GameObject enemy);
    public static event RespawnEnemy Respawn;

    public delegate void RespawnedPlayer(Vector3 position);
    public static event RespawnedPlayer Respawned;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = gameObject.transform.position;
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
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potion"))
        {
            //Debug.Log("Potion Drank");
            potion = true;
            time = 0.0f;
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Enemy"))
        {
            if (potion)
            {
                Respawn(collision.gameObject);
            }
            else
            {
                lives--;
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
    }
}
