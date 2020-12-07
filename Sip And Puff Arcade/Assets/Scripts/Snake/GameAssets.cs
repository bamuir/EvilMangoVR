using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Initializes the snake head, body and food sprites.
 */
public class GameAssets : MonoBehaviour
{
    public static GameAssets i;

    private void Awake()
    {
        i = this;
    }
    public Sprite snakeHeadSprite;
    public Sprite Body;
    public Sprite foodSprite;

}
