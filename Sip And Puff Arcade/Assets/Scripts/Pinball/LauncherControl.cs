using System;
using System.Collections;
using UnityEngine;

public class LauncherControl : MonoBehaviour
{
    public float speedFactor = 1.0f;
    private float moved = 0.0f;
    private Rigidbody rigidBody;

    private const float MAX_MOVE = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack))
        {
            if (moved <= MAX_MOVE)
            {
                float moving = speedFactor;               
                moved += moving;
                rigidBody.MovePosition(rigidBody.position + Vector3.forward * moving);
            }
        } else if (moved > 0.0f)
        {
            float moving = Math.Min(speedFactor * 30f, moved);
            moved -= moving;
            rigidBody.MovePosition(rigidBody.position - Vector3.forward * moving);
        }
    }
}
