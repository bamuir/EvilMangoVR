using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ButtonCode
{
    KeyLeft,
    KeyRight,
    KeyBack,
    KeyFoward,
    KeyComboOne,
    KeyComboTwo,
    KeyComboThree,
    KeyExit
}

/**
 * This class translates from available input sources to a generic type 'TranslationLayer.KeyCode'
 * Should be actively updated as needed with new patterns for the Sip and Puff device.
 * Largely aliases the Input public functions
 */
public class TranslationLayer : MonoBehaviour
{
    public VRControllerAdapter VRAdapter;
    public static TranslationLayer instance = null;
    public SipAndPuffAdapter SipAndPuffAdapter;

    private void Start()
    {
        
    }

    private void Awake()
    {
        if(TranslationLayer.instance == null)
        {
            TranslationLayer.instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        
    }

    /**
     * Returns true in the frame where the button is pressed
     */
    public bool GetButtonDown(ButtonCode key)
    {
        KeyCode systemKey;
        switch(key)
        {
            case ButtonCode.KeyLeft:
                systemKey = KeyCode.LeftArrow;
                break;
            case ButtonCode.KeyRight:
                systemKey = KeyCode.RightArrow;
                break;
            case ButtonCode.KeyBack:
                systemKey = KeyCode.DownArrow;
                break;
            case ButtonCode.KeyFoward:
                systemKey = KeyCode.UpArrow;
                break;
            case ButtonCode.KeyComboOne:
                systemKey = KeyCode.Alpha1;
                break;
            case ButtonCode.KeyComboTwo:
                systemKey = KeyCode.Alpha2;
                break;
            case ButtonCode.KeyComboThree:
                systemKey = KeyCode.Alpha3;
                break;
            case ButtonCode.KeyExit:
                systemKey = KeyCode.BackQuote;
                break;
            default:
                throw new ArgumentException("Unknown KeyCode");
        }
        return Input.GetKeyDown(systemKey) || VRAdapter.GetButtonDown(key) || SipAndPuffAdapter.GetButtonDown(key);
    }
    
    /**
     * Returns true as long as the button is held
     */
    public bool GetButton(ButtonCode key)
    {
        KeyCode systemKey;
        switch (key)
        {
            case ButtonCode.KeyLeft:
                systemKey = KeyCode.LeftArrow;
                break;
            case ButtonCode.KeyRight:
                systemKey = KeyCode.RightArrow;
                break;
            case ButtonCode.KeyBack:
                systemKey = KeyCode.DownArrow;
                break;
            case ButtonCode.KeyFoward:
                systemKey = KeyCode.UpArrow;
                break;
            case ButtonCode.KeyComboOne:
                systemKey = KeyCode.Alpha1;
                break;
            case ButtonCode.KeyComboTwo:
                systemKey = KeyCode.Alpha2;
                break;
            case ButtonCode.KeyComboThree:
                systemKey = KeyCode.Alpha3;
                break;
            case ButtonCode.KeyExit:
                systemKey = KeyCode.BackQuote;
                break;
            default:
                throw new ArgumentException("Unknown KeyCode");
        }
        return Input.GetKey(systemKey) || VRAdapter.GetButton(key) || SipAndPuffAdapter.GetButton(key);
    }
}
