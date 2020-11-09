using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack) && TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            transform.position = startPosition;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PinballFlipper"))
        {
            // rb.AddForce(collision.impulse, ForceMode.Impulse);
            Debug.Log("rebounding");
            rb.AddForce((collision.collider.gameObject.transform.position - gameObject.transform.position).normalized * 2);
        }
    }
}
