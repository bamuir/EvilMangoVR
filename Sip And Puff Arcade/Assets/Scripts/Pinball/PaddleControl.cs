using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public ButtonCode activeButton;
    public GameObject target;

    private Vector3 rotationAxis;
    public RotationState state { get; private set; }
    private float currentRotationAmount = 0.0f;
    public Direction dir;
    private Rigidbody rigidBody;
    private Vector3 initialPos;
    private Quaternion initialRot;

    public enum Direction
    {
        Clockwise,
        CounterClockwise
    }
    public enum RotationState
    {
        Up,
        Down,
        Rest
    }

    // Start is called before the first frame update
    void Start()
    {
        Quaternion r = target.transform.rotation;
        rotationAxis = new Vector3(r.eulerAngles.x, r.eulerAngles.y, r.eulerAngles.z + transform.eulerAngles.z);
        state = RotationState.Rest;
        rigidBody = GetComponent<Rigidbody>();
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButtonDown(activeButton))
        {
            state = RotationState.Up;
        }

        if (state == RotationState.Up)
        {
            float a = 990 * Time.deltaTime;
            currentRotationAmount += a;
            Quaternion q = Quaternion.AngleAxis(a * (dir == Direction.Clockwise ? 1 : -1), rotationAxis);
            rigidBody.MoveRotation(q * rigidBody.transform.rotation);
            rigidBody.MovePosition(q * (rigidBody.transform.position - target.transform.position) + target.transform.position);
            // transform.RotateAround(target.transform.position, rotationAxis, a * (dir == Direction.Clockwise ? 1 : -1));
            if (currentRotationAmount >= 90)
            {
                state = RotationState.Down;
            }
        }
        else if (state == RotationState.Down)
        {
            float a = 630 * Time.deltaTime * -1;
            currentRotationAmount += a;
            if (currentRotationAmount <= 0)
            {
                a -= currentRotationAmount;
                currentRotationAmount = 0;
                state = RotationState.Rest;
                // reset position and rotation
                rigidBody.MoveRotation(initialRot);
                rigidBody.MovePosition(initialPos);
            }
            else
            {
                Quaternion q = Quaternion.AngleAxis(a * (dir == Direction.Clockwise ? 1 : -1), rotationAxis);
                rigidBody.MoveRotation(q * rigidBody.transform.rotation);
                rigidBody.MovePosition(q * (rigidBody.transform.position - target.transform.position) + target.transform.position);
            }
            // transform.RotateAround(target.transform.position, rotationAxis, a * (dir == Direction.Clockwise ? 1 : -1));
        }
    }
}
