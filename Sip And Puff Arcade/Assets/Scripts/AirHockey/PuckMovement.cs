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
    public GameObject playerGoal;
    public GameObject enemyGoal;
    public GameObject gameBoundary;
    public Material OOBMat;
    public GameObject table;
    public GameObject enemyStriker;
    public enum PuckState
    {
        green, red, blue
    };
    
    private PuckState state;
    private Rigidbody p;
    private Vector3 puckStart;
    private Vector3 strikerStart;
    private float oobTimer = 0;
    private bool oob = false;

    // Enables a minor seeking effect towards the enemy goal
    private readonly bool slidingShiftEnabled = true;

    // Prevents the puck from being stuck bouncing horizontally forever. Highly recommend that this not be turned off at the same time as slidingShift
    private readonly bool minimumZSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(defaultMat);
        puckStart = puck.transform.position;
        p = puck.GetComponent<Rigidbody>();
        state = PuckState.blue;
        p.velocity = RandomVector(new Vector3(-1,0,-1), new Vector3(1,0,1));
        strikerStart = enemyStriker.transform.position;
    }

    private void LateUpdate()
    {
        if (p.velocity.magnitude > maxSpeed)
        {
            p.velocity = Vector3.ClampMagnitude(p.velocity, maxSpeed);
        }
        if(TranslationLayer.instance.GetButtonDown(ButtonCode.KeyBack))
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
        if(state == PuckState.green && slidingShiftEnabled)
        {
            p.AddForce(GetGoalVector(enemyGoal)*.035f);
        }
        else if(state == PuckState.red && slidingShiftEnabled)
        {
            p.AddForce(GetGoalVector(playerGoal)*.035f);
        }
        else if(slidingShiftEnabled || minimumZSpeed)
        {
            if (p.velocity.z < 0.2f)
            {
                if (p.velocity.z == 0) p.velocity = new Vector3(p.velocity.x, p.velocity.y, 0.01f);
                p.AddForce((p.velocity.normalized + new Vector3(0, 0, p.velocity.z).normalized).normalized * 0.2f);
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
        state = PuckState.blue;
        enemyStriker.transform.position = strikerStart;
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
        else if (collision.collider.gameObject.CompareTag("AirHockeyBumper"))
        {
            string name = collision.collider.gameObject.name;
            if (name.Contains("Corner"))
            {
                p.velocity = new Vector3(-1 * p.velocity.x, p.velocity.y, -1 * p.velocity.z);
            }
            else if (name.Contains("Top") || name.Contains("Bottom"))
            {
                p.velocity = new Vector3(p.velocity.x, p.velocity.y, -1 * p.velocity.z);
            }
            else if (name.Contains("Right") || name.Contains("Left"))
            {
                p.velocity = new Vector3(-1 * p.velocity.x, p.velocity.y, p.velocity.z);
            }
        }
        if (p.velocity.magnitude > maxSpeed)
        {
            p.velocity = Vector3.ClampMagnitude(p.velocity, maxSpeed);
        }
        if (collision.collider.gameObject.CompareTag("Striker"))
        {
            if(collision.collider.gameObject.name.CompareTo("EnemyStriker") == 0)
            {
                state = PuckState.red;
            }
            else
            {
                state = PuckState.green;
            }
            Material[] mat = gameObject.GetComponent<MeshRenderer>().materials;
            mat[1] = collision.collider.gameObject.GetComponent<MeshRenderer>().material;
            gameObject.GetComponent<MeshRenderer>().materials = mat;
            gameObject.GetComponent<Rigidbody>().AddForce((collision.collider.gameObject.transform.position - gameObject.transform.position).normalized * 2.0f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Striker"))
        {
            Vector3 direction = (puck.transform.position - collision.collider.gameObject.transform.position).normalized;
            p.AddForce(direction * 2, ForceMode.VelocityChange);
        }
        if (p.velocity.magnitude > maxSpeed)
        {
            p.velocity = Vector3.ClampMagnitude(p.velocity, maxSpeed);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("AirhockeyBoundaries"))
        {
            oob = true;
            ChangeTableColor(OOBMat);
            p.velocity = new Vector3(0, 0, 0);
            puck.transform.position = puckStart;
        }
    }

    private void ChangeTableColor(Material m)
    {
        Material[] mat = table.GetComponent<MeshRenderer>().materials;
        mat[3] = m;
        mat[4] = m;
        table.GetComponent<MeshRenderer>().materials = mat;
    }

    private Vector3 GetGoalVector(GameObject goal)
    {
        Vector3 deltaPos = goal.transform.position - puck.transform.position;
        return deltaPos.normalized;
    }
}
