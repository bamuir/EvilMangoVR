using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;

    int currentPoint = 0;
    public float speed = 0.3f;

    Rigidbody2D rb;
    Vector2 direction;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (transform.position != waypoints[currentPoint].position)
        {
            Vector2 point = Vector2.MoveTowards(transform.position, waypoints[currentPoint].position, speed);

            GetComponent<Rigidbody2D>().MovePosition(point);
        }
        else
        {
            currentPoint++;
            currentPoint %= waypoints.Length;
        }

        Vector2 direction = waypoints[currentPoint].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", direction.x);
        GetComponent<Animator>().SetFloat("DirY", direction.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }

}
