using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBumperColor : MonoBehaviour
{
    public Material defaultColor;
    public Material contactColor;


    public void Collide()
    {
        GetComponent<MeshRenderer>().material = contactColor;
    }

    public void ScheduleReset()
    {
        Invoke(nameof(ResetColor), 0.25f);
    }

    private void ResetColor()
    {
        GetComponent<MeshRenderer>().material = defaultColor;
    }
}
