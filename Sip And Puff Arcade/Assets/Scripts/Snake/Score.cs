using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


/**
 * Labels for menu items, score, length, etc are initialized and displayed from this class. 
 * If a user starts the game, gets rid of the start menu. If the user chooses the difficulty
 * selection, bring up that menu and allow user to change speed. 
 */
public class Score : MonoBehaviour
{
    private static Text score;
    private static Text length;
    private static Text startButton;
    private static Text difficultyButton;
    private static Text snakeLabel;
    private static Text count;
    private static Text speedLabel;
    private static Text speed;


    private static Image start;
    private static Image selection;

    private static int speedNum;
    
    

    private void Awake()
    {
        // initialize menu / game labels (start, difficulty, score, length, etc)
        score = transform.Find("Score").GetComponent<Text>();
        length = transform.Find("Length").GetComponent<Text>();
        startButton = transform.Find("StartButton").GetComponent<Text>();
        difficultyButton = transform.Find("DiffButton").GetComponent<Text>();
        count = transform.Find("Countdown").GetComponent<Text>();
        snakeLabel = transform.Find("SnakeLabel").GetComponent<Text>();
        speedLabel = transform.Find("SpeedLabel").GetComponent<Text>();
        speed = transform.Find("Speed").GetComponent<Text>();
        start = transform.Find("Start").GetComponent<Image>();
        selection = transform.Find("Selection").GetComponent<Image>();

        // labels for the difficulty menu and coutdown are not displayed at start
        count.enabled = false;
        speed.enabled = false;
        speedLabel.enabled = false;

        // difficulty / speed start at lowest value
        speedNum = 1;      
    }

    private void Update()
    {
        // if game has started, disable menu labels
        if (!GameHandler.getAtMenu())
        {
            start.enabled = false;
            selection.enabled = false;
            startButton.enabled = false;
            difficultyButton.enabled = false;
            snakeLabel.enabled = false;
        }
    
        if (GameHandler.getAlive())
        {
            score.text = GameHandler.GetScore().ToString();
            length.text = GameHandler.GetLength().ToString();  
        }

        // if snake has died, display 'game over'
        else
        {
            int zero = 0;
            score.text = "GAME OVER :(";
            GameHandler.setAlive();
        }


    }

    // change posistion of box that represents the user's selection on the start menu
    public static void MoveSelection(bool select)
    {
        
        // if we the select is on 'start' button
        if(select)
            selection.transform.position = new Vector3(31.2f, 1.8f, 0);

        // else, selection is on the 'difficulty' button
        else
            selection.transform.position = new Vector3(34.8f, 1.8f, 0);
    }

    // count down at start of game
    public static void CountDown(int num)
    {
        if (num == 0)
        {
            count.enabled = false;
            return;
        }

        count.enabled = true;
        count.transform.position = new Vector3(35, 3, 0);
        count.text = num.ToString();
    }

    // changes difficulty
    public static bool SetSpeed(int i)
    {
        bool changed = false;

        // if speed is from 1-10, change it.
        if (speedNum + i < 11 && speedNum + i > 0)
        {
            speedNum += i;
            changed = true;
        }

        speed.text = speedNum.ToString();

        return changed;
        
    }

    // when user exits the difficutly menu, bring back the original start menu
    public static void BringBackMenu()
    {
        start.enabled = true;
        selection.enabled = true;
        startButton.enabled = true;
        difficultyButton.enabled = true;
        snakeLabel.enabled = true;

        speed.enabled = false;
        speedLabel.enabled = false;

    }

    // if user toggels the difficulty selection, bring up this menu
    public static void DifficultyMenu()
    {
        speed.enabled = true;
        speedLabel.enabled = true;
        selection.enabled = false;
        startButton.enabled = false;
        difficultyButton.enabled = false;
        speed.transform.position = new Vector3(36.5f, 1.8f, 0);
        speedLabel.transform.position = new Vector3(33.5f, 1.8f, 0);
    }
}
