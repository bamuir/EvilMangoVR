using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //switch ()
        //{
        //    case 1:
        //        Console.WriteLine("Case 1");
        //        break;
        //    case 2:
        //        Console.WriteLine("Case 2");
        //        break;
        //    default:
        //        Console.WriteLine("Default case");
        //        break;
        //}

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate (new Vector3 (-2f, 0f, 0f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(new Vector3(2f, 0f, 0f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(new Vector3(0f, -2f, 0f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(new Vector3(0f, 2f, 0f) * Time.deltaTime);
        }
    }
}
