using Microsoft.Win32.SafeHandles;
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
    private Text score;
    private Text length;
    private Text startButton;
    private Text difficultyButton;
    private Text snakeLabel;
    private static Text count;


    private Image start;
    private static Image selection;
    
    

    private void Awake()
    {
        score = transform.Find("Score").GetComponent<Text>();
        length = transform.Find("Length").GetComponent<Text>();
        startButton = transform.Find("StartButton").GetComponent<Text>();
        difficultyButton = transform.Find("DiffButton").GetComponent<Text>();
        count = transform.Find("Countdown").GetComponent<Text>();
        snakeLabel = transform.Find("SnakeLabel").GetComponent<Text>();

        start = transform.Find("Start").GetComponent<Image>();
        selection = transform.Find("Selection").GetComponent<Image>();

        count.enabled = false;
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
            length.text = zero.ToString();
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
}
