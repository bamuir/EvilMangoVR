using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerColor : MonoBehaviour
{
    public Material redNeon;
    public GameObject striker;
    // Start is called before the first frame update
    void Start()
    {
        striker.GetComponent<MeshRenderer>().material = redNeon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
