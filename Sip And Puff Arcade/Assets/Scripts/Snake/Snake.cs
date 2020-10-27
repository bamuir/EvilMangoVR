using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine;
using System;
using System.CodeDom;

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
    private float zDim = 5;

    private int snakeBodySize;
    private List<Vector2> prevPositionList;
    private List<Transform> bodyList;

    private float speed = 0.25f;
    private float negSpeed = -0.25f;

    private bool Alive;

    public void Setup(Grid grid)
    {
        this.grid = grid;
    }

   private void Awake()
    {

        Alive = true;
        snakePos = new Vector2(0, 0);
        snakeStart = new Vector2(33, 2);
        pos_to_screen();

        timePerMove = 0.2f;
        moveTimer = timePerMove;

        // starts off moving to the right.
        moveDirection = new Vector2(speed, 0);

        canMove = true;

        prevPositionList = new List<Vector2>();
        snakeBodySize = 0;

        bodyList = new List<Transform>();



    }

    private void Update()
    {
        if (Alive)
        {
            Movement();
            HandleTime();
        }

        else
            GameHandler.setDead();
        
    }

    private void Movement()
    {

        // change the direction of the snake if it isnt moving back on itself
        // and its direction has been updated by HandleTime().
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyLeft))
        {
            if (moveDirection.x != speed && canMove)
            {
                moveDirection.y = 0;
                moveDirection.x = speed * -1;
                canMove = false;
            }

        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyRight))
        {
            if (moveDirection.x != negSpeed && canMove)
            {
                moveDirection.y = 0;
                moveDirection.x = speed;
                canMove = false;
            }
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            if (moveDirection.y != negSpeed && canMove)
            {
                moveDirection.y = speed;
                moveDirection.x = 0;
                canMove = false;
            }
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack))
        {
            if (moveDirection.y != speed && canMove)
            {
                moveDirection.y = -1 * speed;
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
                makeBody();
                
            }

            // if the size of list is greater than the size of the body, remove the last index.
            if (prevPositionList.Count >= snakeBodySize + 1)
            {
                prevPositionList.RemoveAt(snakeBodySize);
            }

            holdList = new List<Vector2>() { snakePos };
            holdList.AddRange(prevPositionList);

            grid.SnakeBodyPos(holdList);

            for (int i = 0; i < bodyList.Count; i++)
            {
                Vector3 bodyPos = new Vector3(prevPositionList[i].x, prevPositionList[i].y, zDim);
                bodyList[i].position = bodyPos;
            }

        }

        foreach (Vector2 bodyPart in prevPositionList)
        {
            if (snakePos == bodyPart)
            {
                Alive = false;
            }
        }

        if (Math.Abs(snakePos.x - snakeStart.x) > 2.75)
        {
            Alive = false;
            return;
        }

        if (Math.Abs(snakePos.y - snakeStart.y) > 2.75)
        {
            Alive = false;
            return;
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

    private void makeBody()
    {

            GameObject snakeBody = new GameObject("Body", typeof(SpriteRenderer));

            snakeBody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.Body;
            snakeBody.layer = 8;
            bodyList.Add(snakeBody.transform);
        

    }

}
