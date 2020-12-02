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

    // Update is called once per frame
    void Update()
    {
        

        //print("Update called  with toyObj:" + toyObj.tag);

    }

    private void FixedUpdate()
    {
        //if (toyObj.transform.localPosition.y <
        //    craneGameBounds.transform.localPosition.y)
        //{
        //    toyObj.transform.localPosition = new Vector3(toyObj.transform.localPosition.x,
        //                                        toyObj.transform.localPosition.y + 0.00001f,
        //                                        toyObj.transform.localPosition.z);
        //    toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //}

        //if(clawGrab == true )
        //{
        //    CreateFixedJoint();
        //}

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

    private void OnCollisionEnter(Collision collision)
    {

        //print("Collision has started");

    }

    private void OnCollisionExit(Collision collision)
    {
        
        clawGrab = false;
        
       // print("Collision has exited");

    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!toyObj.GetComponent<FixedJoint>() && other.tag == clawObj.tag)
        {
            CreateFixedJoint();
        }

       // print("OnTriggerEnter called by: " + other.tag);

    }

    private void CreateFixedJoint()
    {
        fixedJoint = toyObj.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = clawObj.GetComponent<Rigidbody>();
        fixedJoint.anchor = clawObj.transform.localPosition;

        print("Fixed Joint made for " + toyObj.tag);
    }

    private void RemoveFixedJoint()
    {
        Destroy(toyObj.GetComponent<FixedJoint>());
        toyObj.GetComponent<Rigidbody>().mass = 1f;
        toyObj.GetComponent<Rigidbody>().useGravity = true;
        toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.0001f, 0);
        toyObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0, 0.0001f); 


        clawObj.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        clawObj.GetComponent<Rigidbody>().mass = 0;


    }

    private void ResetToyPosCheck()
    {
        if(toyObj.transform.localPosition.y < yLowerBound - (float)0.009)
        {

          //  WaitTime(3);
            toyObj.transform.localPosition = toyStartPos;
            toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            toyObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            print("Toy is out of machine");

        }
    }

    //IEnumerator WaitTime(int time)
    //{
    //    yield return new WaitForSeconds(time);
    //}



}
