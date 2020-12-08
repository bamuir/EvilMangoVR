using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballTeleporter : MonoBehaviour
{
    public GameObject destination;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PinballBall"))
        {
            collider.attachedRigidbody.velocity = Vector3.zero;
            collider.gameObject.transform.position = destination.transform.position;
        }
    }
}
