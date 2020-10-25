using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using System;

public class Snake : MonoBehaviour
{
    // where the snake head should start and where it is currently
    private Vector2 snakeStart; 
    private Vector2 snakePos;
    private Grid grid;

    // keeps track of time for movement
    private float moveTimer; 
    private float timePerMove;

    private Vector2 moveDirection;

    // if the user inputs 2 controls before the timeToMove only the first valid input is considered. 
    private bool canMove;

    // the z dim of our screen in unity.
    private float zDim = 6.475f;

    private int snakeBodySize;
    private List<Vector2> prevPositionList;
    
   public void Setup(Grid grid)
    {
        this.grid = grid;
    }

   private void Awake()
    {
        snakePos = new Vector2(0, 0);
        snakeStart = new Vector2(5, 2.5f);
        pos_to_screen();

        timePerMove = 0.35f;
        moveTimer = timePerMove;

        // starts off moving to the right.
        moveDirection = new Vector2(0.05f, 0);

        canMove = true;

        prevPositionList = new List<Vector2>();
        snakeBodySize = 0;

        

    }

    private void Update()
    {
        Movement();
        HandleTime();
    }

    private void Movement()
    {
        // change the direction of the snake if it isnt moving back on itself
        // and its direction has been updated by HandleTime().
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyLeft))
        {
            if (moveDirection.x != 0.05f && canMove)
            {
                moveDirection.y = 0;
                moveDirection.x = -0.05f;
                canMove = false;
            }

        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyRight))
        {
            if (moveDirection.x != -0.05f && canMove)
            {
                moveDirection.y = 0;
                moveDirection.x = 0.05f;
                canMove = false;
            }
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            if (moveDirection.y != -0.05f && canMove)
            {
                moveDirection.y = 0.05f;
                moveDirection.x = 0;
                canMove = false;
            }
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack))
        {
            if (moveDirection.y != 0.05f && canMove)
            {
                moveDirection.y = -0.05f;
                moveDirection.x = 0;
                canMove = false;
            }
        }

       

    }

    private void HandleTime()
    {
        // increase move timer
        moveTimer += Time.deltaTime;
        // keeps track of when snake as ate
        bool snakeAte = false;

        // What we send to our Grid class
        List<Vector2> holdList;

        // update snake posisition once per timePerMove
        if (moveTimer >= timePerMove)
        {
            // insert the previos posistion into our list
            prevPositionList.Insert(0, snakePos);
            
            snakePos += moveDirection;
            moveTimer -= timePerMove;
            canMove = true;

            // send position of snake to our Grid classa and receive if the snake has ate.
            snakeAte = grid.SnakeAte(snakePos);
            
            // if we have ate, increase body size.
            if (snakeAte)
            {
                snakeBodySize++;
                
            }

            // if the size of list is greater than the size of the body, remove the last index.
            if (prevPositionList.Count >= snakeBodySize + 1)
            {
                prevPositionList.RemoveAt(snakeBodySize);
            }

            for(int i = 0; i < prevPositionList.Count; i++)
            {
                Vector2 hold = prevPositionList[i];
                World_Sprite worldSprite =  World_Sprite.Create(new Vector3(hold.x, hold.y, zDim), Vector3.one * .04f, Color.white);
                FunctionTimer.Create(worldSprite.DestroySelf, timePerMove);
            }

            holdList = new List<Vector2>() { snakePos };
            holdList.AddRange(prevPositionList);

            grid.SnakeBodyPos(holdList);

        }
        
        


        // update snake position in Unity
        transform.position = new Vector3(snakePos.x, snakePos.y, zDim);

        // send position of snake to our Grid class and receive if the snake has ate.
        snakeAte = grid.SnakeAte(snakePos);


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
