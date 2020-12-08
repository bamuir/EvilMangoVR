using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SipAndPuffAdapter : MonoBehaviour
{
    public UnitySerialPort serialPort;
    public static SipAndPuffAdapter instance;
    private string thisCommand = "";
    private bool changed = false;
    private ConcurrentQueue<string> CommandQueue;

    void OnDestroy()
    {
        serialPort.UpdateEvent.RemoveListener(HandleUpdate);
    }

    void HandleUpdate(string newValue)
    {
        // ignore if queue is too full
        if (CommandQueue.Count < 5 || newValue == "U")
        {
            CommandQueue.Enqueue(newValue);
        }
    }

    void Awake()
    {
        if (SipAndPuffAdapter.instance == null)
        {
            serialPort.UpdateEvent.AddListener(HandleUpdate);
            SipAndPuffAdapter.instance = this;
            DontDestroyOnLoad(this.gameObject);
            CommandQueue = new ConcurrentQueue<string>();
        }
    }

    void Update()
    {
        if (CommandQueue.Count > 0)
        {
            if (!CommandQueue.TryDequeue(out string nextCommand) || string.IsNullOrWhiteSpace(nextCommand)) return;
            if (nextCommand == "U")
            {
                changed = false;
            }
            else
            {
                changed = nextCommand != thisCommand;
            }
            thisCommand = nextCommand;
        }
    }

    public bool GetButtonDown(ButtonCode keyCode)
    {
        if (!changed)
        {
            return false;
        }
        string latestData = thisCommand;

        switch (keyCode)
        {
            case ButtonCode.KeyFoward:
                return latestData == "F";
            case ButtonCode.KeyRight:
                return latestData == "R";
            case ButtonCode.KeyBack:
                return latestData == "B";
            case ButtonCode.KeyLeft:
                return latestData == "L";
            case ButtonCode.KeyExit:
                return latestData == "ESC";
            case ButtonCode.KeyComboOne:
                return latestData == "C1";
            case ButtonCode.KeyComboTwo:
                return latestData == "C2";
            case ButtonCode.KeyComboThree:
                return latestData == "C3";
            default:
                return false;
        }

    }

    public bool GetButton(ButtonCode keyCode)
    {
        string latestData = thisCommand;
        switch (keyCode)
        {
            case ButtonCode.KeyFoward:
                return latestData == "F";
            case ButtonCode.KeyRight:
                return latestData == "R";
            case ButtonCode.KeyBack:
                return latestData == "B";
            case ButtonCode.KeyLeft:
                return latestData == "L";
            case ButtonCode.KeyExit:
                return latestData == "ESC";
            case ButtonCode.KeyComboOne:
                return latestData == "C1";
            case ButtonCode.KeyComboTwo:
                return latestData == "C2";
            case ButtonCode.KeyComboThree:
                return latestData == "C3";
            default:
                return false;
        }
    }
}
