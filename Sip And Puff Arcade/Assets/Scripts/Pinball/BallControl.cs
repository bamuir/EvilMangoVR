using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack) && TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            transform.position = startPosition;
        }
    }
}
