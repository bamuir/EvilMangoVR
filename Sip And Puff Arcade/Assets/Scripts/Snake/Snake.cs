using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine;
using System;
using System.CodeDom;


/**
 * Snake movement, menu selections are handled in this class. Also handles where in the game the user
 * currently is (start menu, difficulty menu, in game) and determines if the snake is dead or alive. 
 * Calls 'grid' class to spawn food and determines if the user has moved the snake over the food. 
 */
public class Snake : MonoBehaviour
{
    // where the snake head should start and where it is currently
    private Vector2 snakeStart; 
    private Vector2 snakePos;
    private Grid grid;

    // keeps track of time for movement
    private float moveTimer; 
    private float timePerMove;
    private float countTime;
    private float gameOverTime;
    private float menuTime;
    private float holdTimePerMove;

    // number used for countdown at start of game
    private int countNum;

    // where the snake is pointing
    private Vector2 moveDirection;

    // if the user inputs more than one control before the timeToMove, only the first valid input is considered. 
    private bool canMove;

    // the z dim of our screen in unity.
    private float zDim = 5;

    private int snakeBodySize;
    private List<Vector2> prevPositionList;
    private List<Transform> bodyList;

    // how far the snake moves per timePerMove
    private float moveDistance = 0.25f;
    private float negativeMoveDistance = -0.25f;

    private bool Alive;

    // used to know where the menu select box is
    private bool selectIsOnStart;
    private bool inDifMenu;

    public void Setup(Grid grid)
    {
        this.grid = grid;
    }

   private void Awake()
    {
        // set speed, position, what screen the user is on, etc.
        Alive = true;
        snakePos = new Vector2(0, 0);
        snakeStart = new Vector2(33, 2);
        pos_to_screen();
        selectIsOnStart = true;
        inDifMenu = false;
        timePerMove = 0.5f;
        menuTime = 0.5f;
        countTime = 1;
        moveTimer = menuTime;
        countNum = 3;
        gameOverTime = 2.5f;


        // starts off moving to the right.
        moveDirection = new Vector2(moveDistance, 0);

        // user can update snake position / menu selection
        canMove = true;

        prevPositionList = new List<Vector2>();
        snakeBodySize = 0;
        bodyList = new List<Transform>();
    }

    private void Update()
    {
        // handle menu selection movement
        if (GameHandler.getAtMenu())
        {
            Menu();
        }

        // if user just selected 'start', enabled the countdown
        else if (GameHandler.getCount())
        {
            CountDown();
        }

        // if the snake is alive and the game has started.
        else if (Alive)
        {
            Movement();
            HandleTime();
        }

        // if snake has died
        else if (!Alive)
        {
            GameHandler.setDead();
            GameOver();
        }
        
    }

    // reset game variables, bring up menu screen
    private void GameOver()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= gameOverTime)
        {
            GameHandler.reset();
            Score.BringBackMenu();
            transform.position = new Vector3(0, 0, 0);
            grid.HideFood();
            deleteBody();

            // reset all variables excet for the difficulty / speed of the snake
            holdTimePerMove = timePerMove;
            Awake();
            timePerMove = holdTimePerMove;
        }
    }

    // countdown at the start of the game (3, 2, 1)
    private void CountDown()
    {
        moveTimer += Time.deltaTime;

        if(moveTimer >= countTime)
        {
            if (countNum == 0)
            {
                GameHandler.setCountBool(false);
                moveTimer = timePerMove;
                Score.CountDown(0);
                return;
            }

            Score.CountDown(countNum);
            countNum--;
            moveTimer -= countTime;
        }


    }

    // control for the start menu.
    // 3 checks are done: Is 'start' toggled, is 'difficulty' toggled, is user in difficulty menu.
    private void Menu()
    {
        // increase move timer
        moveTimer += Time.deltaTime;

        if (moveTimer >= menuTime)
        {
           
            // move left if user is currently toggeling 'difficutly' section and is in start menu
            // decrease speed if user is in difficulty menu
            if (TranslationLayer.instance.GetButton(ButtonCode.KeyLeft))
            {
                moveTimer -= menuTime;
                if (!inDifMenu && !selectIsOnStart)
                {
                    selectIsOnStart = !selectIsOnStart;
                    Score.MoveSelection(selectIsOnStart);
                }

                else if(inDifMenu)
                {
                    if (Score.SetSpeed(-1))
                        timePerMove += .045f;
                }
            }

            // move right if user is currently toggeling 'start' section and is in start menu
            // increase speed if user is in difficulty menu
            else if (TranslationLayer.instance.GetButton(ButtonCode.KeyRight))
            {
                moveTimer -= menuTime;
                if (!inDifMenu && selectIsOnStart)
                {
                    selectIsOnStart = !selectIsOnStart;
                    Score.MoveSelection(selectIsOnStart);
                }

                else if(inDifMenu)
                {
                    if (Score.SetSpeed(1))
                        timePerMove -= .045f;
                }
            }

            // start game if user is toggeling 'start', bring up difficulty menu if user is toggeling 
            // 'difficulty', go back to start menu if user is in difficulty menu.
            else if (TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
            {
                moveTimer -= menuTime;
                // if user selected start
                if (selectIsOnStart && !inDifMenu)
                {
                    grid.SpawnFood();
                    GameHandler.StartGame();
                    return;
                }

                // if user selected difficulty
                else if (!inDifMenu)
                {
                    Score.DifficultyMenu();
                    inDifMenu = true;
                }

                // if user wants to exit difficulty menu.
                else if(inDifMenu)
                {
                    inDifMenu = false;
                    Score.BringBackMenu();
                }
            }

            else if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack))
            {
               
            }

            // if no button is pushed
            moveTimer -= Time.deltaTime;
        }
    }

    private void Movement()
    {

        // change the direction of the snake if it isnt moving back on itself
        // and its direction has been updated by HandleTime().
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyLeft))
        {
            if (moveDirection.x != moveDistance && canMove)
            {
                moveDirection.y = 0;
                moveDirection.x = moveDistance * -1;
                canMove = false;
            }

        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyRight))
        {
            if (moveDirection.x != negativeMoveDistance && canMove)
            {
                moveDirection.y = 0;
                moveDirection.x = moveDistance;
                canMove = false;
            }
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            if (moveDirection.y != negativeMoveDistance && canMove)
            {
                moveDirection.y = moveDistance;
                moveDirection.x = 0;
                canMove = false;
            }
        }
       
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack))
        {
            if (moveDirection.y != moveDistance && canMove)
            {
                moveDirection.y = -1 * moveDistance;
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
            // insert the previos posistion into our list (to determine where body parts go)
            prevPositionList.Insert(0, snakePos);
            
            snakePos += moveDirection;
            moveTimer -= timePerMove;
            canMove = true;

            // send position of snake to our Grid class and receive if the snake has ate.
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

    // Translates the position to the Unity. Used just in case things need to be moved in game.
    // Start value can be updated if we move the screen, arcade machine, etc.
    private void pos_to_screen()
    {

        float x = (snakePos.x / 400) + snakeStart.x;
        float y = (snakePos.y / 400) + snakeStart.y;

        snakePos.x = x;
        snakePos.y = y;

    }

    // make a body part if food is ate
    private void makeBody()
    {

            GameObject snakeBody = new GameObject("Body", typeof(SpriteRenderer));
            snakeBody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.Body;
            snakeBody.layer = 8;
            bodyList.Add(snakeBody.transform);

    }

    // if game ends, delete all body parts before next game.
    private void deleteBody()
    {
        foreach(Transform b in bodyList)
        {
            Destroy(b.gameObject);
        }
    }

}
