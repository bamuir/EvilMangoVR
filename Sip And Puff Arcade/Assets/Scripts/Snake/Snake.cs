using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 start;
    private Vector2 gridPos;
    private float gridMoveTimer;
    private float gridMoveTimerMax;

    private Vector2 gridMoveDir;

   private void Awake()
    {
        gridPos = new Vector2(0, 0);
        start = new Vector2(5, 2.5f);
        pos_to_screen();

        gridMoveTimerMax = 1f;
        gridMoveTimer = gridMoveTimerMax;

        gridMoveDir = new Vector2(0.05f, 0);

    }

    private void Update()
    {

        if (TranslationLayer.instance.GetButton(ButtonCode.KeyLeft))
        {
            gridMoveDir.y = 0;
            gridMoveDir.x = -0.05f;

        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyRight))
        {
            gridMoveDir.y = 0;
            gridMoveDir.x = 0.05f;
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            gridMoveDir.y = 0.05f;
            gridMoveDir.x = 0;
        }
        else if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack))
        {
            gridMoveDir.y = -0.05f;
            gridMoveDir.x = 0;
        }

        gridMoveTimer += Time.deltaTime;

        if(gridMoveTimer >= gridMoveTimerMax)
        {
            gridPos += gridMoveDir;
            gridMoveTimer -= gridMoveTimerMax;
        }
        transform.position = new Vector3(gridPos.x, gridPos.y, 6.49f);

    }

    private void pos_to_screen()
    {

        float x = (gridPos.x / 400) + start.x;
        float y = (gridPos.y / 400) + start.y;

        gridPos.x = x;
        gridPos.y = y;

    }
}
