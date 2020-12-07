using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 4.0f;
    Vector2 dest = Vector2.zero;
    //enum Direction { Stopped, Left, Right, Up, Down};
    //Direction currentDirection = Direction.Right;
    Vector2 currentDirection;
    Vector2 nextDirection;

    //float move;
    Vector2 movement;

    Node currentNode;
    Node targetNode;
    Node previousNode;
    public Node initialNode;

    private void OnEnable()
    {
        RespawnPlayer.Respawned += ResetPlayer;
        ResetGame.ResetPuffman += ResetPlayer;
    }

    private void OnDisable()
    {
        RespawnPlayer.Respawned -= ResetPlayer;
        ResetGame.ResetPuffman -= ResetPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;

        currentNode = initialNode;
        currentDirection = Vector2.right;
        ChangeDirection(currentDirection);
    }

    // Update is called once per frame
    private void Update()
    {
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyFoward))
        {
            ChangeDirection(Vector2.up);
        }
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyRight))
        {
            ChangeDirection(Vector2.right);
        }
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyBack))
        {
            ChangeDirection(Vector2.down);
        }
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyLeft))
        {
            ChangeDirection(Vector2.left);
        }
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (targetNode != currentNode && targetNode != null)
        {
            if (IsInBounds())
            {
                currentNode = targetNode;

                transform.position = currentNode.transform.position;

                Node n = Valid(nextDirection);

                if (n)
                {
                    currentDirection = nextDirection;
                }
                else
                {
                    n = Valid(currentDirection);
                }

                if (n)
                {
                    targetNode = n;
                    previousNode = currentNode;
                    currentNode = null;
                }
                else
                {
                    currentDirection = Vector2.zero;
                }
            }
            else
            {
                transform.position += (Vector3)(currentDirection * speed) * Time.deltaTime;
            }
        }
    }

    void ChangeDirection(Vector2 dir)
    {
        if (dir != currentDirection)
        {
            nextDirection = dir;
        }

        if (currentNode)
        {
            Node n = Valid(dir);

            if (n)
            {
                currentDirection = dir;
                targetNode = n;
                previousNode = currentNode;
                currentNode = null;
            }
        }
    }

    Node Valid(Vector2 dir)
    {
        Node potentialNode = null;

        for (int i = 0; i < currentNode.neighbors.Count; i++)
        {
            if (currentNode.valid[i] == dir)
            {
                potentialNode = currentNode.neighbors[i];
                break;
            }
        }

        return potentialNode;
    }

    void ResetPlayer(Vector3 startingPos)
    {
        gameObject.transform.position = startingPos;

        currentNode = initialNode;
        previousNode = null;
        targetNode = null;
        currentDirection = Vector2.right;
        nextDirection = Vector2.zero;
        ChangeDirection(currentDirection);
    }

    float LengthBetweenNodes(Vector2 target)
    {
        Vector2 v = target - (Vector2)previousNode.transform.position;
        return v.sqrMagnitude;
    }

    bool IsInBounds()
    {
        float target = LengthBetweenNodes(targetNode.transform.position);
        float pos = LengthBetweenNodes(transform.position);

        return pos > target;
    }
}
