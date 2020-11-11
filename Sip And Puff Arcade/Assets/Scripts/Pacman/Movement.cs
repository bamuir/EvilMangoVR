using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    Vector2 dest = Vector2.zero;
    enum Direction { Stopped, Left, Right, Up, Down};
    Direction currentDirection = Direction.Right;

    float move;
    Vector2 movement;

    private void Awake()
    {
        RespawnPlayer.Respawned += ResetPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dir = dest - (Vector2)transform.position;
        
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyFoward) && Valid(Vector2.up))
        {
            currentDirection = Direction.Up;
        }
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyRight) && Valid(Vector2.right))
        {
            currentDirection = Direction.Right;
        }
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyBack) && Valid(-Vector2.up))
        {
            currentDirection = Direction.Down;
        }
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyLeft) && Valid(-Vector2.right))
        {
            currentDirection = Direction.Left;
        }


        if (currentDirection == Direction.Up)
        {
            movement = new Vector2 (0, speed * 1.0f);
            dir = new Vector2(0, 0.2f);
        }
        if (currentDirection == Direction.Right)
        {
            movement = new Vector2(speed * 1.0f, 0);
            dir = new Vector2(0.2f, 0);
        }
        if (currentDirection == Direction.Down)
        {
            movement = new Vector2(0, -speed * 1.0f);
            dir = new Vector2(0, -0.2f);
        }
        if (currentDirection == Direction.Left)
        {
            movement = new Vector2(-speed * 1.0f, 0);
            dir = new Vector2(-0.2f, 0);
        }

        GetComponent<Rigidbody2D>().velocity = movement;

        //Vector2 dir = dest - (Vector2)transform.position;
        //GetComponent<Animator>().SetFloat("DirX", dir.x);
        //GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 dir)
    {
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(position + dir, position);
        return (hit.collider == GetComponent<Collider2D>());
    }

    void ResetPlayer(Vector3 startingPos)
    {
        gameObject.transform.position = startingPos;
        currentDirection = Direction.Right;
    }
}
