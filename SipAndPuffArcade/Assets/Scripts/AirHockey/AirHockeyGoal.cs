using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirHockeyGoal : MonoBehaviour
{
    public GameObject puck;
    public GameObject table;
    public float gameWinTime = 1;
    public Material opponentGoalMat;

    private bool recentGoal = false;
    private float goalTime = 0;
    private Material defTableMat;

    private void Start()
    {
        defTableMat = table.GetComponent<MeshRenderer>().materials[3];
    }

    private void Update()
    {
        if(recentGoal)
        {
            goalTime += Time.deltaTime;
            puck.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            if (goalTime > gameWinTime)
            {
                recentGoal = false;
                puck.GetComponent<PuckMovement>().GameReset();
                goalTime = 0;
                ChangeTableColor(defTableMat);
            }
        }
        if (gameObject.GetComponent<BoxCollider>().bounds.Contains(puck.transform.position))
        {
            recentGoal = true;
            ChangeTableColor(opponentGoalMat);
            puck.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    private void ChangeTableColor(Material m)
    {
        Material[] mat = table.GetComponent<MeshRenderer>().materials;
        mat[3] = m;
        mat[4] = m;
        table.GetComponent<MeshRenderer>().materials = mat;
    }
}
