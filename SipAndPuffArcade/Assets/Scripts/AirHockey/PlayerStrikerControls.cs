using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikerControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyLeft))
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z + 0.01f);
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyRight))
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z - 0.01f);
        }
        if(gameObject.transform.localPosition.z > 0.825f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0.825f);
        }
        if (gameObject.transform.localPosition.z < -0.825f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -0.825f);
        }
    }
}
