using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

/**
 * Handles label values such as score, length, etc. Handles game state (is snake alive, has game started
 * what menu the user is on)
 */
public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    private static int score;
    private static int length;
    private static bool alive;
    private static bool atMenu;
    private static bool count;

    private void Awake()
    {
        instance = this;
        score = 0;
        length = 0;
        alive = true;
        atMenu = true;
    }

    [SerializeField] private Snake snake;
    [SerializeField] private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        snake.Setup(grid);
        
        grid.Setup(snake);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int GetScore()
    {
        return score;
    }

    public static void IncreaseScore()
    {
        score += 50;
    }

    public static int GetLength()
    {
        return length;
    }

    public static void IncreaseLength()
    {
        length++;
    }

    public static bool getAlive()
    {
        return alive;
    }

    public static void setDead()
    {
        alive = false;
    }

    public static void setAlive()
    {
        alive = true;
    }

    public static void StartGame()
    {
        atMenu = false;
        count = true;
        alive = true;
    }

    public static bool getAtMenu()
    {
        return atMenu;
    }

    public static void setAtMenu()
    {
        atMenu = true;
    }

    public static void setCountBool(bool done)
    {
        count = done;
    }

    public static bool getCount()
    {
        return count;
    }

    public static void reset()
    {
        score = 0;
        length = 0;
        alive = true;
        atMenu = true;
    }




}
