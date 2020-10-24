using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // where the snake head should start and where it is currently
    private Vector2 snakeStart; 
    private Vector2 snakePos; 

    // keeps track of time for movement
    private float moveTimer; 
    private float timePerMove;

    private Vector2 moveDirection;

   private void Awake()
    {
        snakePos = new Vector2(0, 0);
        snakeStart = new Vector2(5, 2.5f);
        pos_to_screen();

        timePerMove = 1f;
        moveTimer = timePerMove;

        moveDirection = new Vector2(0.05f, 0);

    }

    private void Update()
    {

        if (TranslationLayer.instance.GetButton(ButtonCode.KeyLeft))
        {
            moveDirection.y = 0;
            moveDirection.x = -0.05f;

        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyRight))
        {
            moveDirection.y = 0;
            moveDirection.x = 0.05f;
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            moveDirection.y = 0.05f;
            moveDirection.x = 0;
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack))
        {
            moveDirection.y = -0.05f;
            moveDirection.x = 0;
        }

        // increase move timer
        moveTimer += Time.deltaTime;

        // update snake posisition once per timePerMove
        if(moveTimer >= timePerMove)
        {
            snakePos += moveDirection;
            moveTimer -= timePerMove;
        }

        // update snake position in Unity
        transform.position = new Vector3(snakePos.x, snakePos.y, 6.475f);

    }

    /**
     * Translates the position to the Unity. Used just in case things need to be moved in game.
     * Start value can be updated if we move the screen, arcade machine, etc.
     */
    private void pos_to_screen()
    {

        float x = (snakePos.x / 400) + snakeStart.x;
        float y = (snakePos.y / 400) + snakeStart.y;

        snakePos.x = x;
        snakePos.y = y;

    }
}
