using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    public delegate GameObject IncrementScore(int score);
    public static event IncrementScore Score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Score(1);
            gameObject.SetActive(false);
        }
    }
}
