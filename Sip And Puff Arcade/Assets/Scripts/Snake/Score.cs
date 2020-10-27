using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    private Text score;
    private Text length;


    private void Awake()
    {
        score = transform.Find("Score").GetComponent<Text>();
        length = transform.Find("Length").GetComponent<Text>();

    }

    private void Update()
    {
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
}
