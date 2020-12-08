using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toyMovement : MonoBehaviour
{

    public GameObject toyObj;
    public FixedJoint fixedJoint;
    public GameObject clawObj;
    public GameObject craneGameBox;

    public bool clawGrab;
    public float yLowerBound;
    public Vector3 toyStartPos;


    // Start is called before the first frame update
    void Start()
    {
        yLowerBound = (craneGameBox.transform.localPosition.y - (float)(craneGameBox.GetComponent<BoxCollider>().size.y / 2.0));
        toyStartPos = toyObj.transform.localPosition;


    }

    private void FixedUpdate()
    {
       
        ResetToyPosCheck();
        if (toyObj.GetComponent<FixedJoint>())
        {
            toyObj.GetComponent<Rigidbody>().mass = 0;
            clawObj.GetComponent<Rigidbody>().mass = 0;

            toyObj.GetComponent<Rigidbody>().useGravity = false;
            clawObj.GetComponent<Rigidbody>().useGravity = false;

            toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            clawObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyComboThree))
        {
            RemoveFixedJoint();
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        clawGrab = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!toyObj.GetComponent<FixedJoint>() && other.tag == clawObj.tag)
        {
            CreateFixedJoint();
        }

    }

    private void CreateFixedJoint()
    {
        fixedJoint = toyObj.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = clawObj.GetComponent<Rigidbody>();
        fixedJoint.anchor = clawObj.transform.localPosition;

    }

    private void RemoveFixedJoint()
    {
        Destroy(toyObj.GetComponent<FixedJoint>());
        toyObj.GetComponent<Rigidbody>().mass = 1f;
        toyObj.GetComponent<Rigidbody>().useGravity = true;
        toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.0001f, 0);
        toyObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0, 0.0001f); 


        clawObj.GetComponent<Rigidbody>().velocity = new Vector3(0,0.1f,0);
        clawObj.GetComponent<Rigidbody>().mass = 0;


    }

    private void ResetToyPosCheck()
    {
        if(toyObj.transform.localPosition.y < yLowerBound - (float)0.009)
        {

            toyObj.transform.localPosition = toyStartPos;
            toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            toyObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
    }



}
