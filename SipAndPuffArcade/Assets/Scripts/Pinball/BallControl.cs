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
    private bool launched = false;
    private float stallTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // resets if you press up and down at the same time. Mostly for debugging since it can't be done in the Sip and Puff
        if (TranslationLayer.instance.GetButton(ButtonCode.KeyBack) && TranslationLayer.instance.GetButton(ButtonCode.KeyFoward))
        {
            ResetPosition();
        }
        if (launched && rb.velocity == Vector3.zero)
        {
            stallTime += Time.deltaTime;

            if (stallTime >= 3)
            {
                stallTime = 0;
                
                Vector3 randomForce = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)).normalized;
                rb.AddForce(randomForce / 3);
            }
        } else
        {
            stallTime = 0;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "ResetTrigger")
        {
            Invoke(nameof(ResetPosition), 0.5f);
        }

        if (collision.gameObject.CompareTag("PinballLauncherEnd"))
        {
            rb.useGravity = true;
            launched = true;
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

        if (collision.gameObject.TryGetComponent(out LauncherControl launcher))
        {
            if (launched && launcher.state == LauncherControl.MoveState.Rest)
            {
                rb.useGravity = true;
                launched = false;
            } else if (launcher.state == LauncherControl.MoveState.Forward)
            {
                Vector3 dir = collision.impulse.normalized;
                rb.useGravity = false;
                rb.AddForce(-dir * 5, ForceMode.VelocityChange);
                rb.AddForce(dir, ForceMode.Acceleration);
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

    public void ResetPosition()
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        launched = false;
    }
}
