using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;
    private Queue<Task> tasks = new Queue<Task>();
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
            ResetPosition();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "ResetTrigger")
        {
            Invoke(nameof(ResetPosition), 0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PinballBumper"))
        {
            // reverse velocity and accelerate
            Vector3 difference = collision.gameObject.transform.position - transform.position;
            rb.AddForce(difference.normalized * 5, ForceMode.VelocityChange);
            collision.gameObject.GetComponent<SetBumperColor>().Collide();
        }

        if (collision.gameObject.CompareTag("PinballCushion"))
        {
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            float speedInDirection = Vector3.Project(rb.velocity, direction).magnitude;
            if (speedInDirection >= 0.75f)
            {
                rb.AddForce(direction * -2, ForceMode.VelocityChange);
                collision.gameObject.GetComponent<SetBumperColor>().Collide();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PinballCushion") || collision.gameObject.CompareTag("PinballBumper"))
        {
            collision.gameObject.GetComponent<SetBumperColor>().ScheduleReset();
        }
    }

    void ResetPosition()
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
    }
}
