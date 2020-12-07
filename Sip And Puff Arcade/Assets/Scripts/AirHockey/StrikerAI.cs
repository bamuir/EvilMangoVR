using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerAI : MonoBehaviour
{
    public GameObject puck;
    public GameObject striker;
    public float maxSpeed = 0.01f;
    public float maxAcceleration = 0.1f;
    private Rigidbody p;
    private Vector3 strikerStart;

    // Start is called before the first frame update
    void Start()
    {
        p = striker.GetComponent<Rigidbody>();
        strikerStart = striker.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 steering = Seek(striker, puck, maxAcceleration);
        p.AddForce(steering, ForceMode.Acceleration);
    }

    private void LateUpdate()
    {
        p.velocity = Vector3.ClampMagnitude(p.velocity, maxSpeed);
    }

    private void OnCollisionExit(Collision collision)
    { 
        if(collision.collider.gameObject.CompareTag("AirhockeyBoundaries"))
        {
            striker.transform.position = strikerStart;
        }
    }

    private Vector3 Seek(GameObject character, GameObject target, float maxAcceleration)
    {
        Vector3 steering;
        Vector3 delta = target.transform.position - character.transform.position;
        if(delta.z > 1.5f)
        {
            //Return to goalie position if the puck is far away.
            delta = strikerStart - character.transform.position;
            maxAcceleration *= 2;
        }
        steering = delta;
        steering = new Vector3(steering.x, 0, 0);
        steering = steering.normalized;
        steering *= maxAcceleration;
        return steering;
    }
}
