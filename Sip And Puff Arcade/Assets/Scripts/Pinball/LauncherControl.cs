using System;
using System.Collections;
using UnityEngine;

public class LauncherControl : MonoBehaviour
{
    public float speedFactor = 1.0f;
    private float moved = 0.0f;
    private Rigidbody rigidBody;
    public MoveState state = MoveState.Rest;
    private Vector3 originalPosition;
    
    public enum MoveState
    {
        Rest,
        Back,
        BackRest,
        Forward
    }

    private const float MAX_MOVE = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyBack) && state == MoveState.Rest)
        {
            state = MoveState.Back;
        }
        if (state == MoveState.Back)
        {
            if (moved < MAX_MOVE)
            {
                float moving = speedFactor;               
                moved += moving;
                rigidBody.MovePosition(rigidBody.position + Vector3.forward * moving);
            } else
            {
                state = MoveState.Forward;
            }
        } 
        if (state == MoveState.Forward)
        {
            // applies a linear acceleration
            float moving = Math.Min(speedFactor * 3, moved);
            moved -= moving;
            rigidBody.MovePosition(rigidBody.position - Vector3.forward * moving);
            if (moved == 0)
            {
                transform.position = originalPosition;
                state = MoveState.Rest;
            }
        }
    }
}
