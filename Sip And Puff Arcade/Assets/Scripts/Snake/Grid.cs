using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System.Diagnostics;

public class Grid : MonoBehaviour
{
    private Vector2 foodPos;
    // width and height of our screen
    private int width = 20;
    private int height = 20;
    // the position of our snake.
    private Vector2 snakePosition;
    // the z dim of our screen in unity.
    private float zDim = 5;
    // represent the min, max of the bounds for the snake screen.
    private Vector2 sideBound = new Vector2(30.5f, 35.5f);
    private Vector2 upBound = new Vector2(-0.5f, 5.5f);
    private Snake snake;
    private List<Vector2> snakeBodyPos;
 
    
    public void Setup(Snake snake)
    {
        this.snake = snake;
    }

    private void Awake()
    {
        // initialzie snake body.
        snakeBodyPos = new List<Vector2>();

        // update food position in Unity
        //SpawnFood();
       
    }

    private void Update()
    {
        
    }

    public void SpawnFood()
    {
        // spawn food in a new location on screen if it isnt in the same position as our snake.
        do
        {
            Vector2 randPos = new Vector2(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height));
            foodPos = new Vector2(randPos.x * 0.25f + sideBound.x, randPos.y * 0.25f + upBound.x);
        } while (snakeBodyPos.IndexOf(foodPos) != -1);
       

        // update food position in Unity
        transform.position = new Vector3(foodPos.x, foodPos.y, zDim);

        
    }

    public bool SnakeAte(Vector2 snakePos)
    {
        // if the snake has moved, update our version of the snakes location.
        // if the snake has 'ate' the food, spawn food in a new loctation. 
        snakePosition = snakePos;
        if (snakePos == foodPos)
        {
            SpawnFood();

            // update score
            GameHandler.IncreaseScore();

            // update length
            GameHandler.IncreaseLength();
            return true;
        }

        return false;
    }

    // get the list of that contains the position of the snake body parts.
    public void SnakeBodyPos(List<Vector2> bodyPos)
    {
        snakeBodyPos = bodyPos;
       
    }

    
}

