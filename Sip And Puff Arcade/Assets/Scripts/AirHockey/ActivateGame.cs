using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActivateGame : MonoBehaviour
{

    public GameObject TeleportPoint;
    public GameObject GameCamera;
    public GameSwitch switcher;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyFoward) && !switcher.inGame)
        {
            switcher.inGame = true;
        }
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack) && switcher.inGame)
        {
            switcher.inGame = false;
        }
    }
}
