using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PuckHitStriker : MonoBehaviour
{
    public float bounceForce = 2;
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
        if(collision.collider.gameObject.CompareTag("Striker"))
        {
            Material[] mat = gameObject.GetComponent<MeshRenderer>().materials;
            mat[1] = collision.collider.gameObject.GetComponent<MeshRenderer>().material;
            gameObject.GetComponent<MeshRenderer>().materials = mat;
            gameObject.GetComponent<Rigidbody>().AddForce((collision.collider.gameObject.transform.position - gameObject.transform.position).normalized * bounceForce);
        }
    }
}
