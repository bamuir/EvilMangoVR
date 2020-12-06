using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


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

        count.enabled = false;
        speed.enabled = false;
        speedLabel.enabled = false;

        speedNum = 1;
        
    }

    private void Update()
    {
        // if we are at the start menu...
        if (GameHandler.getAtMenu())
        {
            
        }

        else
        {
            start.enabled = false;
            selection.enabled = false;
            startButton.enabled = false;
            difficultyButton.enabled = false;
            snakeLabel.enabled = false;
        }

        // if countdown done, disable.

        if (GameHandler.getAlive())
        {
            
            score.text = GameHandler.GetScore().ToString();
            length.text = GameHandler.GetLength().ToString();  
        }

        else
        {
            int zero = 0;
            score.text = "GAME OVER :(";
            //length.text = zero.ToString();
            GameHandler.setAlive();
        }


    }

    public static void MoveSelection(bool select)
    {
        
        // if we the select is on 'start' button
        if(select)
            selection.transform.position = new Vector3(31.2f, 1.8f, 0);

        // else, selection is on the 'difficulty' button
        else
            selection.transform.position = new Vector3(34.8f, 1.8f, 0);
    }

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

    public static void ResetSpeed()
    {
        speedNum = 1;
        speed.text = 1.ToString();
    }

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
