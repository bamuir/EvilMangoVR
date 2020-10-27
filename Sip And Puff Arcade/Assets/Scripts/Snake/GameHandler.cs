using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class GameHandler : MonoBehaviour
{
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
}
