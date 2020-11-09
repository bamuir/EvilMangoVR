using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool inGame;

    private int gameindex;

    private Dictionary<int, GameObject> gamelist;

    private void Start()
    {
        // Start at spawnb
        gameindex = 2;

        inGame = false;

        // Add all games to list
        gamelist = new Dictionary<int, GameObject>();
        gamelist.Add(0, Spawn);
        gamelist.Add(1, Airhockey);
        gamelist.Add(2, Pinball);
        gamelist.Add(3, Game3);
        gamelist.Add(4, Game4);
        gamelist.Add(5, Game5);
        gamelist.Add(6, Game6);

        Player.transform.position = gamelist[gameindex].transform.position;
        Vector3 newPos = gamelist[gameindex].transform.eulerAngles;

        // set rotation and flip 180
        Player.transform.eulerAngles = new Vector3(
            newPos.x,
            newPos.y + 180,
            newPos.z
            );
    }

    // Update is called once per frame
    void Update()
    {
        // Sip simulation
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyLeft) && !inGame)
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
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyRight) && !inGame)
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

    }

    // An always positive mod
    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
