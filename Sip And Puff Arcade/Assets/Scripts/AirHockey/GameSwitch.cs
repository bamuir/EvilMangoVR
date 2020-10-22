using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSwitch : MonoBehaviour
{
    public GameObject Player;
    public GameObject Spawn;
    public GameObject Airhockey;
    public GameObject Pinball;
    public GameObject Game3;
    public GameObject Game4;
    public GameObject Game5;
    public GameObject Game6;

    private int gameindex;

    private Dictionary<int, GameObject> gamelist;

    private void Start()
    {
        // Start at spawnb
        gameindex = 0;

        // Add all games to list
        gamelist = new Dictionary<int, GameObject>();
        gamelist.Add(0, Spawn);
        gamelist.Add(1, Airhockey);
        gamelist.Add(2, Pinball);
        gamelist.Add(3, Game3);
        gamelist.Add(4, Game4);
        gamelist.Add(5, Game5);
        gamelist.Add(6, Game6);
    }

    // Update is called once per frame
    void Update()
    {
        // Sip simulation
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyLeft))
        {
            gameindex = mod(gameindex - 1, 6);
            Player.transform.position = gamelist[gameindex].transform.position;
            Vector3 newPos = gamelist[gameindex].transform.eulerAngles;

            // set rotation and flip 180
            Player.transform.eulerAngles = new Vector3(
                newPos.x,
                newPos.y + 180,
                newPos.z
                );

        }

        // Puff
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyRight))
        {
            gameindex = mod(gameindex + 1, 6);
            Player.transform.position = gamelist[gameindex].transform.position;
            Vector3 newPos = gamelist[gameindex].transform.eulerAngles;

            // set rotation and flip 180
            Player.transform.eulerAngles = new Vector3(
                newPos.x,
                newPos.y + 180,
                newPos.z
                );

        }

        // Enter Game
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyFoward))
        {
            switch(gameindex)
            {
                case 0:
                    break;

                case 1:
                    // load airhockey
                    SceneManager.LoadScene("Breakout_AirHockey", LoadSceneMode.Single);
                    break;

                case 2:
                    // load pinball
                    SceneManager.LoadScene("Breakout_Pinball", LoadSceneMode.Single);
                    break;

                case 3:
                    // load Arcade Cabinent
                    SceneManager.LoadScene("Breakout_Arcade", LoadSceneMode.Single);
                    break;

                case 4:
                    // load cranegame
                    SceneManager.LoadScene("Breakout_CraneGame", LoadSceneMode.Single);
                    break;

                case 5:
                    // load snake
                    SceneManager.LoadScene("Breakout_Snake", LoadSceneMode.Single);
                    break;

                case 6:
                    // load shooting gallery
                    SceneManager.LoadScene("Breakout_ShootingGallery", LoadSceneMode.Single);
                    break;

                default:
                    break;
            }
        }
    }

    // An always positive mod
    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
