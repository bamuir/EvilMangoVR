using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRControllerAdapter : MonoBehaviour
{
    public SteamVR_Input_Sources hand;

    public SteamVR_Action_Boolean forwardPressed;
    public SteamVR_Action_Boolean rightPressed;
    public SteamVR_Action_Boolean leftPressed;
    public SteamVR_Action_Boolean backPressed;

    public bool GetButtonDown(ButtonCode keyCode)
    {
        switch (keyCode)
        {
            case ButtonCode.KeyFoward:
                return forwardPressed.GetStateDown(hand);
            case ButtonCode.KeyRight:
                return rightPressed.GetStateDown(hand);
            case ButtonCode.KeyBack:
                return backPressed.GetStateDown(hand);
            case ButtonCode.KeyLeft:
                return leftPressed.GetStateDown(hand);
            default:
                throw new ArgumentException("Unknown keyCode");
        }
    }

    public bool GetButton(ButtonCode keyCode)
    {
        switch (keyCode)
        {
            case ButtonCode.KeyFoward:
                return forwardPressed.GetState(hand);
            case ButtonCode.KeyRight:
                return rightPressed.GetState(hand);
            case ButtonCode.KeyBack:
                return backPressed.GetState(hand);
            case ButtonCode.KeyLeft:
                return leftPressed.GetState(hand);
            default:
                throw new ArgumentException("Unknown keyCode");
        }
    }
}
