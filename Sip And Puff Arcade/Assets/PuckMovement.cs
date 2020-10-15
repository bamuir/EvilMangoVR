using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuckMovement : MonoBehaviour
{
    public GameObject puck;
    public Material defaultMat;
    public float minSpeed = 1;
    public float acceleration = 1.001f;
    public float startSpeed = 2;
    private Rigidbody p;
    private Vector3 puckStart;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(defaultMat);
        puckStart = puck.transform.position;
        p = puck.GetComponent<Rigidbody>();
        p.velocity = RandomVector(new Vector3(-1,0,-1), new Vector3(1,0,1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if(p.velocity.magnitude < minSpeed)
        {
            p.AddForce(p.velocity.normalized * acceleration, ForceMode.Acceleration);
        }
    }

    private Vector3 RandomVector(Vector3 min, Vector4 max)
    {
        return new Vector3(UnityEngine.Random.Range(min.x, max.x), 0, UnityEngine.Random.Range(min.z, max.z)).normalized * startSpeed;
    }

    public void Reset()
    {
        puck.transform.position = puckStart;
        p.velocity = RandomVector(new Vector3(-1, 0, -1), new Vector3(1, 0, 1));
        SetColor(defaultMat);
    }

    private void SetColor(Material m)
    {
        Material[] mat = gameObject.GetComponent<MeshRenderer>().materials;
        mat[1] = m;
        gameObject.GetComponent<MeshRenderer>().materials = mat;
    }
}
