using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float accelerationFactor = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PinballBall"))
        {
            other.attachedRigidbody.velocity *= accelerationFactor;
        }
    }
}
