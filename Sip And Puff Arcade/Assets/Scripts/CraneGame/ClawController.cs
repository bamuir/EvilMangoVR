using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    public GameObject craneObj;
    public GameObject craneGameBounds;

    public float yUpperBound;
    public float yLowerBound;
    public float xRightBound;
    public float xLeftBound;
    public float zFarBound;
    public float zNearBound;


    // Start is called before the first frame update
    void Start()
    {
        yUpperBound = (craneGameBounds.transform.localPosition.y + (float)(craneGameBounds.GetComponent<BoxCollider>().size.y / 2.0));
        yLowerBound = (craneGameBounds.transform.localPosition.y - (float)(craneGameBounds.GetComponent<BoxCollider>().size.y / 2.0) + (float)0.003);
        xRightBound = (craneGameBounds.transform.localPosition.x + (float)(craneGameBounds.GetComponent<BoxCollider>().size.x / 2.0) - (float)0.002);
        xLeftBound = (craneGameBounds.transform.localPosition.x - (float)(craneGameBounds.GetComponent<BoxCollider>().size.x / 2.0) + (float)0.002);
        zFarBound = (craneGameBounds.transform.localPosition.z - (float)(craneGameBounds.GetComponent<BoxCollider>().size.z / 2.0) + (float)0.002);
        zNearBound = (craneGameBounds.transform.localPosition.z + (float)(craneGameBounds.GetComponent<BoxCollider>().size.z / 2.0) - (float)0.002);
    }

    // Update is called once per frame
    void Update()
    {
         
        CraneAdjustInBounds();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate (new Vector3 (2f, 0f, 0f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(new Vector3(-2f, 0f, 0f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(new Vector3(0f, 0f, 2f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow) )
        {
            this.transform.Translate(new Vector3(0f, 0f, -2f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) )
        {
            this.transform.Translate(new Vector3(0f, 2f, 0f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) )
        {
            this.transform.Translate(new Vector3(0f, -2f, 0f) * Time.deltaTime);
        }

    }

    public bool ClawInBounds()
    {

        

        print("Calc Y UpperBound = " + yUpperBound);
        print("Crane local y = " + craneObj.transform.localPosition.y);

        if ( craneObj.transform.localPosition.y > yLowerBound)
        {
            print("In Machine Bounds");
            return true;
        }
        else
        {
            print("Out of Machine Bounds");

            return false;
        }
    }

    public void CraneAdjustInBounds()
    {
        if (craneObj.transform.localPosition.y < yLowerBound)
        {
            craneObj.transform.localPosition = new Vector3(craneObj.transform.localPosition.x, (yLowerBound + (float)0.0001), craneObj.transform.localPosition.z);
        }
        else if (craneObj.transform.localPosition.y > yUpperBound)
        {
            craneObj.transform.localPosition = new Vector3(craneObj.transform.localPosition.x, (yUpperBound - (float)0.0001), craneObj.transform.localPosition.z);
        }
        else if (craneObj.transform.localPosition.x > xRightBound)
        {
            craneObj.transform.localPosition = new Vector3((xRightBound - (float)0.0001), craneObj.transform.localPosition.y, craneObj.transform.localPosition.z);
        }
        else if (craneObj.transform.localPosition.x < xLeftBound)
        {
            craneObj.transform.localPosition = new Vector3((xLeftBound + (float)0.0001), craneObj.transform.localPosition.y, craneObj.transform.localPosition.z);
        }
        else if (craneObj.transform.localPosition.z < zFarBound)
        {
            craneObj.transform.localPosition = new Vector3(craneObj.transform.localPosition.x, craneObj.transform.localPosition.y, (zFarBound + (float)0.0001));
        }
        else if (craneObj.transform.localPosition.z > zNearBound)
        {
            craneObj.transform.localPosition = new Vector3(craneObj.transform.localPosition.x, craneObj.transform.localPosition.y, (zNearBound - (float)0.0001));
        }

    }
}
