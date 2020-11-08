using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prizeBoxImpact : MonoBehaviour
{

    public GameObject toyObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 toyObjVelVec = collision.gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 toyObjAngVel = collision.gameObject.GetComponent<Rigidbody>().angularVelocity;
        toyObjVelVec = new Vector3(toyObjVelVec.x, toyObjVelVec.y, toyObjVelVec.z + (float)0.2);

        collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 200);

        print(collision.gameObject.tag + " has collided with prize floor");


    }

    private void OnTriggerEnter(Collider other)
    {
        //Vector3 toyObjVelVec = other.gameObject.GetComponent<Rigidbody>().velocity;
        //toyObjVelVec = new Vector3(toyObjVelVec.x, toyObjVelVec.y, toyObjVelVec.z + (float)0.02);

        //print(other.gameObject.tag + " has collided with prize floor");
    }
}
