using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirHockeyGoal : MonoBehaviour
{
    public GameObject puck;
    
    private Rigidbody rb;
    private void Start()
    {
        rb = puck.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Puck"))
        {
            puck.GetComponent<PuckMovement>().Reset();
        }
    }
}
