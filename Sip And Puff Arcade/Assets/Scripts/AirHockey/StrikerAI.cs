using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerAI : MonoBehaviour
{
    public GameObject puck;
    public float trackingSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //delta x
        //slide towards as speed
        float deltaX = puck.transform.position.x - transform.position.x;
        float deltaMove = trackingSpeed * Time.deltaTime;
        float newX;
        if (Mathf.Abs(deltaX) <= deltaMove) newX = puck.transform.position.x;
        else if (deltaX > 0) newX = transform.position.x + deltaMove;
        else newX = transform.position.x - deltaMove;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
