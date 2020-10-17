using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuckMovement : MonoBehaviour
{
    public GameObject puck;
    public Material defaultMat;
    public float maxSpeed = 3;
    public float acceleration = 1.001f;
    public float startSpeed = 2;
    public GameObject goal1;
    public GameObject goal2;
    public GameObject gameBoundary;
    public Material OOBMat;
    public GameObject table;
    private Rigidbody p;
    private Vector3 puckStart;
    private float oobTimer = 0;
    private bool oob = false;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(defaultMat);
        puckStart = puck.transform.position;
        p = puck.GetComponent<Rigidbody>();
        p.velocity = RandomVector(new Vector3(-1,0,-1), new Vector3(1,0,1));
        p.AddExplosionForce(0.1f, puckStart, 10, 0, ForceMode.Acceleration);
    }

    private void LateUpdate()
    {
        if(p.velocity.magnitude > maxSpeed)
        {
            Vector3.ClampMagnitude(p.velocity, maxSpeed);
        }
        if(!gameBoundary.GetComponent<BoxCollider>().bounds.Contains(puck.transform.position) || TranslationLayer.instance.GetButtonDown(ButtonCode.KeyBack))
        {
            oob = true;
            ChangeTableColor(OOBMat);
            p.velocity = new Vector3(0, 0, 0);
            puck.transform.position = puckStart;
        }
        if (oob)
        {
            oobTimer += Time.deltaTime;
            if (oobTimer > 1)
            {
                oob = false;
                puck.GetComponent<PuckMovement>().GameReset();
                oobTimer = 0;
                ChangeTableColor(defaultMat);
                GameReset();
            }
        }
    }

    private Vector3 RandomVector(Vector3 min, Vector4 max)
    {
        return new Vector3(UnityEngine.Random.Range(min.x, max.x), 0, UnityEngine.Random.Range(min.z, max.z)).normalized * startSpeed;
    }

    public void GameReset()
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
    
    private float Distance(GameObject a, GameObject b)
    {
        return Mathf.Abs((a.transform.localPosition - b.transform.localPosition).magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Striker"))
        {
            Vector3 direction = (puck.transform.position - collision.collider.gameObject.transform.position).normalized;
            p.AddForce(direction * 2, ForceMode.VelocityChange);
        }
        else if(collision.collider.gameObject.CompareTag("AirHockeyBumper"))
        {
            Vector3 delta = gameObject.transform.position - collision.GetContact(0).point;
            p.AddForce(delta.normalized * 4, ForceMode.VelocityChange);
        }
        if (p.velocity.magnitude > maxSpeed)
        {
            p.velocity = Vector3.ClampMagnitude(p.velocity, maxSpeed);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Striker"))
        {
            Vector3 direction = (puck.transform.position - collision.collider.gameObject.transform.position).normalized;
            p.AddForce(direction * 2, ForceMode.VelocityChange);
        }
        else if (collision.collider.gameObject.CompareTag("AirHockeyBumper"))
        {
            Vector3 delta = gameObject.transform.position - collision.GetContact(0).point;
            p.AddForce(delta.normalized * 4, ForceMode.VelocityChange);
        }
        if (p.velocity.magnitude > maxSpeed)
        {
            p.velocity = Vector3.ClampMagnitude(p.velocity, maxSpeed);
        }

    }
    private void ChangeTableColor(Material m)
    {
        Material[] mat = table.GetComponent<MeshRenderer>().materials;
        mat[0] = m;
        table.GetComponent<MeshRenderer>().materials = mat;
    }
}
