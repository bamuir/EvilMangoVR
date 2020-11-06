using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toyMovement : MonoBehaviour
{

    public GameObject craneGameBounds;
    public GameObject toyObj;
    public FixedJoint fixedJoint;
    public GameObject clawObj;
    public bool clawGrab;
    

    // Start is called before the first frame update
    void Start()
    {

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

        if (toyObj.GetComponent<FixedJoint>())
        {
            toyObj.GetComponent<Rigidbody>().mass = 0;
            clawObj.GetComponent<Rigidbody>().mass = 0;

            toyObj.GetComponent<Rigidbody>().useGravity = false;
            clawObj.GetComponent<Rigidbody>().useGravity = false;

            toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            clawObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Space))
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

        if (toyObj.transform.localPosition.y ==
         craneGameBounds.GetComponent<BoxCollider>().bounds.min.y)
        {
            toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Tab) && collision.collider.tag == clawObj.tag)
        {
            clawGrab = true;
        }

        //print("Collision is happening with :" + toyObj.tag);

    }

    private void OnTriggerExit(Collider other)
    {



        //other.transform.localPosition = new Vector3(-0.05749f, yLowerNorm, -0.0825f);

        //if(!craneGameBounds.GetComponent<BoxCollider>().bounds.Contains(other))

        //  print("OnTriggerExit called by: " + other.tag);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!toyObj.GetComponent<FixedJoint>() && other.tag == clawObj.tag)
        {
            CreateFixedJoint();
        }

        print("OnTriggerEnter called by: " + other.tag);

    }

    private void CreateFixedJoint()
    {
        fixedJoint = toyObj.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = clawObj.GetComponent<Rigidbody>();
        fixedJoint.anchor = clawObj.transform.localPosition;
       // fixedJoint.axis = Vector3.forward;

        print("Fixed Joint made for " + toyObj.tag);
    }

    private void RemoveFixedJoint()
    {
        Destroy(toyObj.GetComponent<FixedJoint>());
        toyObj.GetComponent<Rigidbody>().mass = 1f;
        toyObj.GetComponent<Rigidbody>().useGravity = true;
        toyObj.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.0001f, 0);
        toyObj.GetComponent<Rigidbody>().rotation = Quaternion.identity;


        clawObj.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        clawObj.GetComponent<Rigidbody>().mass = 0;


    }



}
